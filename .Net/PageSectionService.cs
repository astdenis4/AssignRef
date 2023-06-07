using Microsoft.AspNetCore.Mvc.RazorPages;
using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Domain.CertificationResults;
using Sabio.Models.Domain.PageSections;
using Sabio.Models.Domain.PageTranslations;
using Sabio.Models.Requests.PageSections;
using Sabio.Services.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace Sabio.Services
{
    public class PageSectionService : IPageSectionService
    {
        IDataProvider _data = null;
        public PageSectionService(IDataProvider data)
        {
            _data = data;
        }
        public void Delete(int id)
        {

            string procName = "[dbo].[PageSection_Delete]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)

            {
                col.AddWithValue("@Id", id);
            },

            returnParameters: null);

        }
        public int Add(PageSectionAddRequest model)
        {
            int id = 0;

            string procName = "[dbo].[PageSection_Insert]";

            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);

                    SqlParameter IdOut = new SqlParameter("@Id", SqlDbType.Int);
                    IdOut.Direction = ParameterDirection.Output;

                    col.Add(IdOut);
                },

                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;

                    int.TryParse(oId.ToString(), out id);
                }

                );
            return id;
        }
        public PageSection Get(int id)
        {

            string procName = "[dbo].[PageSection_SelectById]";

            PageSection pageSection = null;

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {

                paramCollection.AddWithValue("@Id", id);

            }, delegate (IDataReader reader, short set)

            {
                pageSection = MapSinglePageSection(reader);
            }

            );
            return pageSection;
        }
        public void Update(PageSectionUpdateRequest model)
        {

            string procName = "[dbo].[PageSection_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    col.AddWithValue("@Id", model.Id);

                },
            returnParameters: null);

        }
        public Paged<PageTranslationResult> GetById(int id, int pageIndex, int pageSize)
        {
            
            
            Paged<PageTranslationResult> translationResult = null;

            List<PageTranslationResult> listResult = null;


            int totalCount = 0;
            string procName = "[dbo].[PageTranslations_SelectByLanguagesId]";
            _data.ExecuteCmd(
                procName,
                delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@LanguageId", id);
                    paramCollection.AddWithValue("@PageIndex", pageIndex);
                    paramCollection.AddWithValue("@PageSize", pageSize);
                },
                delegate (IDataReader reader, short set)
                {
                    PageTranslationResult pageResult = new PageTranslationResult();
                    int startingIndex = 0;
                    pageResult = MapTranslationResult(reader, ref startingIndex);
                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex++);
                    }
                    if (listResult == null)
                    {
                        listResult = new List<PageTranslationResult>();
                    }
                    listResult.Add(pageResult);
                }
            );
            if (listResult != null)
            {
                translationResult = new Paged<PageTranslationResult>(
                    listResult,
                    pageIndex,
                    pageSize,
                    totalCount
                    );
            }
            return translationResult;
        }
        public List<PageTranslationJSON> SelectPageTranslationDetailsByLanguageId(int languageId, int? pageTranslationId)
        {
            List<PageTranslationJSON> pageTranslations = null;
            PageTranslationJSON pageTranslationDetails = null;

            string procName = "[dbo].[PageTranslations_SelectByLanguageId_JSON]";

            _data.ExecuteCmd(procName,
            inputParamMapper: delegate (SqlParameterCollection col)
            {
               
                if (pageTranslationId.HasValue)
                {
                    col.AddWithValue("@PageTranslationId", pageTranslationId.Value);
                    col.AddWithValue("@LanguageId", languageId);
                }
            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;

                pageTranslationDetails = MapSinglePageTranslationJSON(reader, ref startingIndex);
                
                if (pageTranslations == null)
                {
                    pageTranslations = new List<PageTranslationJSON>();
                }
                pageTranslations.Add(pageTranslationDetails);
            });

            return pageTranslations;
        }
        public PageTranslationJSON MapSinglePageTranslationJSON(IDataReader reader, ref int startingIndex)
        {
            PageTranslationJSON pageTranslationDetails = new PageTranslationJSON();

            pageTranslationDetails.PageTranslation = new PageTranslation();

            pageTranslationDetails.PageTranslation.Id = reader.GetSafeInt32(startingIndex++);

            pageTranslationDetails.PageTranslation.Language = new LookUp();
            pageTranslationDetails.PageTranslation.Language.Id = reader.GetSafeInt32(startingIndex++);
            pageTranslationDetails.PageTranslation.Language.Name = reader.GetSafeString(startingIndex++);

            pageTranslationDetails.PageTranslation.Link = reader.GetSafeString(startingIndex++);
            pageTranslationDetails.PageTranslation.Name = reader.GetSafeString(startingIndex++);
            pageTranslationDetails.PageTranslation.CreateBy = reader.GetSafeInt32(startingIndex++);
            pageTranslationDetails.PageTranslation.IsActive = reader.GetSafeBool(startingIndex++);
            pageTranslationDetails.PageSections = reader.DeserializeObject<List<PageSectionDetail>>(startingIndex++);

            return pageTranslationDetails;
        }
        private PageTranslationResult MapTranslationResult(IDataReader reader, ref int startingIndex)
        {
            PageTranslationResult singleTranslationResult = new PageTranslationResult();

            singleTranslationResult.PageTranslation = new PageTranslation();
            singleTranslationResult.PageTranslation.Language = new LookUp();

            singleTranslationResult.PageSection = new PageSection();
            singleTranslationResult.PageSectionKey = new PageSectionKey();
            singleTranslationResult.PageSectionContent = new PageSectionContent();

            singleTranslationResult.PageTranslation.Id = reader.GetSafeInt32(startingIndex++);
            singleTranslationResult.PageTranslation.Language.Id = reader.GetSafeInt32(startingIndex++);
            singleTranslationResult.PageTranslation.Language.Name = reader.GetSafeString(startingIndex++);
            singleTranslationResult.PageTranslation.Link = reader.GetSafeString(startingIndex++);
            singleTranslationResult.PageTranslation.Name = reader.GetSafeString(startingIndex++);
            singleTranslationResult.PageTranslation.CreateBy = reader.GetSafeInt32(startingIndex++);
            singleTranslationResult.PageTranslation.IsActive = reader.GetSafeBool(startingIndex++);

            singleTranslationResult.PageSection.Id = reader.GetSafeInt32(startingIndex++);
            singleTranslationResult.PageSection.PageTranslationId = reader.GetSafeInt32(startingIndex++);
            singleTranslationResult.PageSection.Name = reader.GetSafeString(startingIndex++);
            singleTranslationResult.PageSection.Component = reader.GetSafeString(startingIndex++);
            singleTranslationResult.PageSection.IsActive = reader.GetSafeBool(startingIndex++);

            singleTranslationResult.PageSectionKey.Id = reader.GetSafeInt32(startingIndex++);
            singleTranslationResult.PageSectionKey.PageSectionId = reader.GetSafeInt32(startingIndex++);
            singleTranslationResult.PageSectionKey.KeyName = reader.GetSafeString(startingIndex++);

            singleTranslationResult.PageSectionContent.Id = reader.GetSafeInt32(startingIndex++);
            
            singleTranslationResult.PageSectionContent.Text = reader.GetSafeString(startingIndex++);
            singleTranslationResult.PageSectionContent.LanguageId = reader.GetSafeInt32(startingIndex++);

            return singleTranslationResult;
        }
        private static PageSection MapSinglePageSection(IDataReader reader)
        {
            PageSection pageSection;
            int startingIdex = 0;

            pageSection = new PageSection();

            pageSection.Id = reader.GetSafeInt32(startingIdex++);
            pageSection.PageTranslationId = reader.GetSafeInt32(startingIdex++);
            pageSection.Name = reader.GetSafeString(startingIdex++);
            pageSection.Component = reader.GetSafeString(startingIdex++);
            pageSection.IsActive = reader.GetBoolean(startingIdex++);

            return pageSection;
        }
        private static void AddCommonParams(PageSectionAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@PageTranslationId", model.PageTranslationId);
            col.AddWithValue("@Name", model.Name);
            col.AddWithValue("@Component", model.Component);
            col.AddWithValue("@IsActive", model.IsActive);

        }

    }
}