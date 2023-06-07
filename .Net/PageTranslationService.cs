using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models.Domain;
using Sabio.Models;
using Sabio.Models.Domain.PageTranslations;
using Sabio.Models.Requests.PageTranslations;
using Sabio.Services.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class PageTranslationService : IPageTranslationService
    {

        IDataProvider _data = null;
  
        public PageTranslationService(IDataProvider data)
        { _data = data;
       
        }
        public void Delete(int id)

        {
            string procName = "[dbo].[PageTranslations_Delete]";

            _data.ExecuteNonQuery(procName, inputParamMapper: delegate (SqlParameterCollection col)

            {
                col.AddWithValue("@Id", id);
            },

            returnParameters: null);

        }
        public int Add(PageTranslationAddRequest model, int userId)

        {
            int id = 0;

            string procName = "[dbo].[PageTranslations_Insert]";

            _data.ExecuteNonQuery(procName,
               inputParamMapper: delegate (SqlParameterCollection col)
               {
                   AddCommonParams(model, col, userId);

                   SqlParameter IdOut = new SqlParameter("@Id", SqlDbType.Int);
                   IdOut.Direction = ParameterDirection.Output;

                   col.Add(IdOut);
               },

               returnParameters: delegate (SqlParameterCollection returnCollection)
               {
                   object oId = returnCollection["@Id"].Value;

                   int.TryParse(oId.ToString(), out id);
               });

            return id;
        }
        public List<PageTranslation> SelectPageByLanguageDetails(string link, int languageId)
        {
            List<PageTranslation> list = null;

            string procName = "[dbo].[PageTranslations_Select_PageByLanguageDetails]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {

                paramCollection.AddWithValue("@link", link);
                paramCollection.AddWithValue("@LanguageId", languageId);

            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                PageTranslation pageTranslation = Mapper(reader, ref startingIndex);

                if (list == null)
                {

                    list = new List<PageTranslation>();
                }
                list.Add(pageTranslation);
            });
            return list;

        }
        public List<PageTranslation> SelectPageByLanguage(string link, int languageId)
        {
            List<PageTranslation> list = null;

            string procName = "[dbo].[PageTranslations_Select_PageByLanguage]";

            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {

                paramCollection.AddWithValue("@link", link);
                paramCollection.AddWithValue("@LanguageId", languageId);

            }, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                PageTranslation pageTranslation = Mapper(reader, ref startingIndex);

                if (list == null)
                {

                    list = new List<PageTranslation>();
                }  
                list.Add(pageTranslation);
            });

            return list;
        }
        public Paged<PageTranslation> GetPage(int pageIndex, int pageSize)
        {
            Paged<PageTranslation> pagedList = null;
            List<PageTranslation> pageTranslationList = null;
            int totalCount = 0;

            string procName = "[dbo].[PageTranslations_SelectAll]";

            _data.ExecuteCmd(procName,
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@PageIndex", pageIndex);
                    collection.AddWithValue("@PageSize", pageSize);
                },
            singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                PageTranslation pageTranslation = Mapper(reader, ref startingIndex);

                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(startingIndex++);
                }

                if (pageTranslationList == null)
                {
                    pageTranslationList = new List<PageTranslation>();
                }
                pageTranslationList.Add(pageTranslation);
            });

            if (pageTranslationList != null)
            {
                pagedList = new Paged<PageTranslation>(pageTranslationList, pageIndex, pageSize, totalCount);

            }

            return pagedList;
        }
        public void Update(PageTranslationUpdateRequest model, int userId)
        {
            string procName = "[dbo].[PageTranslations_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col, userId);
                    col.AddWithValue("@Id", model.Id);
                },
            returnParameters: null);
        }
        private static PageTranslation Mapper(IDataReader reader, ref int startingIndex)
        {
            PageTranslation model = new PageTranslation();
           
            model.Id = reader.GetSafeInt32(startingIndex++);
            model.Language = new LookUp();
            model.Language.Id = reader.GetSafeInt32(startingIndex++); 
            model.Language.Name = reader.GetSafeString(startingIndex++);
            model.Link = reader.GetSafeString(startingIndex++);
            model.Name = reader.GetSafeString(startingIndex++);                                       
            model.CreateBy = reader.GetSafeInt32(startingIndex++);
            model.IsActive = reader.GetBoolean(startingIndex++);

            return model;
        }
        private static void AddCommonParams(PageTranslationAddRequest model, SqlParameterCollection col, int userId)
        {       
            col.AddWithValue("@Link", model.Link);
            col.AddWithValue("@LanguageId", model.LanguageId);
            col.AddWithValue("@Name", model.Name);
            col.AddWithValue("@CreatedBy", userId);                            
            col.AddWithValue("@IsActive", model.IsActive);

        }
    }
}
