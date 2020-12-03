
namespace Shinetech.Common
{
    public enum ResponseCode
    {
        //Common
        OK = 200,
        Created = 201,
        Accepted = 202,
        NoContent = 204,

        BadRequest = 400,
        Unauthorized = 401,
        PasswordError = 402,
        Forbidden = 403,
        NotFound = 404,
        NotAcceptable = 406,
        Conflict = 409,
        PreconditionFailed = 412,
        UnsupportedMediaType = 415,
        TokenExpired = 421,
        InternalServerError = 500,
        ServiceUnavailable = 503,

    }
}
