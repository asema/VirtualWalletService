using System;
using ApplicationServices.Exceptions;
using Domain.DomaiinException;
using WalletApi.Models.Error;

namespace WalletApi.Extensions
{
    public static class ErrorResponseExtension
    {
        public static ErrorResponse ChangeToError(this Exception exception)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new ErrorModel
            {
                Code = "SYSTEM_ERROR",
                Message = "Unexpected error occured please try again or confirm current operation status"
            });
            return errorResponse;
        }

        public static ErrorResponse ChangeToError(this BadRequestException badRequestException)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new ErrorModel
            {
                Code = badRequestException.Code,
                Message = badRequestException.Message
            });
            return errorResponse;
        }

        public static ErrorResponse ChangeToError(this ArgumentIsNullException argumentIsNullException)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new ErrorModel
            {
                Code = argumentIsNullException.Code,
                Message = argumentIsNullException.Message
            });
            return errorResponse;
        }

        public static ErrorResponse ChangeToError(this NotFoundException notFoundException)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new ErrorModel
            {
                Code = notFoundException.Code,
                Message = notFoundException.Message
            });
            return errorResponse;
        }

        public static ErrorResponse ChangeToError(this UnAuthorizedException unAuthorizedException)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new ErrorModel
            {
                Code = unAuthorizedException.Code,
                Message = unAuthorizedException.Message
            });
            return errorResponse;
        }

        public static ErrorResponse ChangeToError(this ArgumentIsOutOfRangeException unAuthorizedException)
        {
            var errorResponse = new ErrorResponse();
            errorResponse.Errors.Add(new ErrorModel
            {
                Code = unAuthorizedException.Code,
                Message = unAuthorizedException.Message
            });
            return errorResponse;
        }
    }
}
