using Microsoft.AspNetCore.Components;
using Sabio.Services.Interfaces;
using Sabio.Services;
using Sabio.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;
using Sabio.Web.Models.Responses;
using Stripe;
using System;
using Sabio.Models.Requests.PageSections;
using Sabio.Models.Domain.PageSections;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Sabio.Models;
using System.Collections.Generic;

namespace Sabio.Web.Api.Controllers
{

    [Route("api/translations/pages/section")]
    [ApiController]
    public class PageSectionApiController : BaseApiController
    {
        private IPageSectionService _pageSectionService = null;
        private IAuthenticationService<int> _authService = null;
      

        public PageSectionApiController(IPageSectionService pageSectionService, ILogger<PageSectionApiController> logger, IAuthenticationService<int> authService) : base(logger)
        {
            _pageSectionService = pageSectionService;
            _authService = authService;
          

        }

        [HttpDelete("{id:int}")]
        public ActionResult<SuccessResponse> Delete(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _pageSectionService.Delete(id);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

        [HttpPost("")]
        public ActionResult<ItemResponse<int>> Create(PageSectionAddRequest model)
        {
            ObjectResult result = null;

            try
            {

                int userId = _authService.GetCurrentUserId();

                int id = _pageSectionService.Add(model);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };

                result = Created201(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);

                result = StatusCode(500, response);
            }

            return result;
        }

        [HttpGet("{id:int}")]
        public ActionResult<ItemResponse<PageSection>> Get(int id)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
                PageSection pageSection = _pageSectionService.Get(id);

                if (pageSection == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemResponse<PageSection> { Item = pageSection };
                }
            }
            catch (Exception ex)
            {

                iCode = 500;
                base.Logger.LogError(ex.ToString());
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }

            return StatusCode(iCode, response);

        }

        [HttpPut("{id:int}")]
        public ActionResult<SuccessResponse> Update(PageSectionUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                _pageSectionService.Update(model);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

        [HttpGet("language/{id:int}")]
        public ActionResult<ItemResponse<Paged<PageTranslationResult>>> Get(int id, int pageIndex, int pageSize)
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                Paged<PageTranslationResult> pageTranslationResult = _pageSectionService.GetById(id, pageIndex, pageSize);

                if (pageTranslationResult == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Page Translation Result not found");
                }
                else
                {
                    //ItemResponse<Paged<PageTranslationResult>>>
                    response = new ItemResponse<Paged<PageTranslationResult>> { Item = pageTranslationResult };
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }
            return StatusCode(iCode, response);
        }

        [HttpGet("pagetranslation/{id:int}/{languageId:int}")]
        public ActionResult<ItemsResponse<PageTranslationJSON>> GetPageTranslationDetailsByLanguageId(int id, int languageId)
        {
            int iCode = 200;
            BaseResponse response = null;
            try
            {
                List<PageTranslationJSON> pageTranslations = _pageSectionService.SelectPageTranslationDetailsByLanguageId(languageId, id);

                if (pageTranslations == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Page translation details not found");
                }
                else
                {
                    response = new ItemsResponse<PageTranslationJSON> { Items = pageTranslations };
                }
            }
            catch (Exception ex)
            {
                iCode = 500;
                response = new ErrorResponse($"Generic Error: {ex.Message}");
            }
            return StatusCode(iCode, response);
        }
    }
}