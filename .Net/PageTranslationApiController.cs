using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain.PageTranslations;
using Sabio.Models.Requests.PageTranslations;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using Stripe;
using System;
using System.Collections.Generic;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Sabio.Web.Api.Controllers
{

    [Route("api/translations/pages")]
    [ApiController]

    public class PageTranslationApiController : BaseApiController
    {
        
        private IPageTranslationService _pageTranslationService = null;
        private IAuthenticationService<int> _authService = null;

        public PageTranslationApiController(IPageTranslationService pageTranslationService, ILogger<PageTranslationApiController> logger, IAuthenticationService<int> authService) : base(logger)
        {
            _pageTranslationService = pageTranslationService;
            _authService = authService;
        }

        [HttpDelete("{id:int}")]
        public ActionResult<SuccessResponse> Delete(int id)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {

                _pageTranslationService.Delete(id);

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
        public ActionResult<ItemResponse<int>> Create(PageTranslationAddRequest model)
        {
            
            ObjectResult result = null;

            try
            {
              
                int userId = _authService.GetCurrentUserId();

                
                int id = _pageTranslationService.Add(model, userId);
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

        [HttpGet("paginate")]
        public ActionResult<ItemResponse<Paged<PageTranslation>>> GetPage(int pageIndex, int pageSize)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                Paged<PageTranslation> page = _pageTranslationService.GetPage(pageIndex, pageSize);

                if (page == null)
                {
                    code = 404;
                    response = new ErrorResponse("App Resource not found.");
                }
                else
                {
                    response = new ItemResponse<Paged<PageTranslation>> { Item = page };
                }
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
                base.Logger.LogError(ex.ToString());
            }

            return StatusCode(code, response);

        }

        [HttpPut("{id:int}")]

        public ActionResult<SuccessResponse> Update(PageTranslationUpdateRequest model)
        {
            int code = 200;
            BaseResponse response = null;

            try
            {
                int userId = _authService.GetCurrentUserId();
                _pageTranslationService.Update(model, userId);

                response = new SuccessResponse();
            }
            catch (Exception ex)
            {
                code = 500;
                response = new ErrorResponse(ex.Message);
            }

            return StatusCode(code, response);
        }

        [HttpGet("details/{languageId:int}")]
        public ActionResult<ItemsResponse<PageTranslation>> SelectPageByLanguageDetails( int languageId, string link)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {
             
                List<PageTranslation> list = _pageTranslationService.SelectPageByLanguage(link, languageId);

                if (list == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<PageTranslation> { Items = list };
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

        [HttpGet("{languageId:int}")]
        public ActionResult<ItemsResponse<PageTranslation>> SelectPageByLanguage(string link, int languageId)
        {
            int iCode = 200;
            BaseResponse response = null;

            try
            {

                List<PageTranslation> list = _pageTranslationService.SelectPageByLanguage(link, languageId);

                if (list == null)
                {
                    iCode = 404;
                    response = new ErrorResponse("Application Resource not found.");
                }
                else
                {
                    response = new ItemsResponse<PageTranslation> { Items = list };
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

    }
}
