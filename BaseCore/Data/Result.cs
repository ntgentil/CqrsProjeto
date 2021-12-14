using BaseCore.Validation.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaseCore.Data
{
    public interface IResult
    {
        public IEnumerable<Notification> ErrorMessages { get; }
        public bool Success { get; }
        public bool Failure => !Success;
    }

    internal static class InternalErrorMessages
    {
        public static readonly string ErrorObjectIsNotProvidedForFailure =
            "You attempted to create a failure result, which must have an error, but a null error object was passed to the constructor.";

        public static readonly string ErrorObjectIsProvidedForSuccess =
            "You attempted to create a success result, which cannot have an error, but a non-null error object was passed to the constructor.";
    }

    public class Result : IResult
    {
        public IEnumerable<Notification> ErrorMessages { get; }
        public bool Success { get; }
        public bool Failure => !Success;
        public object Data { get; }

        private static readonly Result OkResult = new Result(true, null, new Notification[] { });

        public static Result Ok()
        {
            return OkResult;
        }

        public static Result Ok(object data)
        {
            return new Result(isSuccess: true, data: data, errorMessages: new Notification[] { });
        }

        public T GetData<T>() => (T)Data;

        public static Result Fail(string errorMessage) =>
            new Result(false, null, new Notification("none", errorMessage));

        public static Result Fail(IEnumerable<string> errorMessages) =>
            new Result(false, null, errorMessages.Select(x => new Notification("none", x)).ToArray());

        public static Result Fail(IEnumerable<Notification> notifications) =>
            new Result(false, null, notifications.ToArray());

        private Result(bool isSuccess, object data, params Notification[] errorMessages)
        {
            var doNotExistsErrorMessage = !errorMessages.Any();
            var doExistsErrorMessage = !doNotExistsErrorMessage;

            if (isSuccess)
            {
                if (doExistsErrorMessage)
                    throw new ArgumentException(
                        InternalErrorMessages.ErrorObjectIsProvidedForSuccess,
                        nameof(errorMessages));
            }
            else
            {
                if (doNotExistsErrorMessage)
                    throw new ArgumentNullException(
                        nameof(errorMessages),
                        InternalErrorMessages.ErrorObjectIsNotProvidedForFailure);
            }

            Success = isSuccess;
            ErrorMessages = errorMessages;
            Data = data;
        }
    }

    public class Result<T> : IResult
    {
        public T Data { get; }
        public IEnumerable<Notification> ErrorMessages { get; }
        public bool Success { get; }
        public bool Failure => !Success;

        public static Result<T> Ok(T data) =>
            new Result<T>(true, data, new Notification[] { });

        public static Result<T> Fail(string errorMessage) =>
            new Result<T>(false, default, new Notification("none", errorMessage));

        public static Result<T> Fail(IEnumerable<string> errorMessages) =>
            new Result<T>(false, default, errorMessages.Select(x => new Notification("none", x)).ToArray());

        public static Result<T> Fail(IEnumerable<Notification> notifications) =>
            new Result<T>(false, default, notifications.ToArray());

        public static Result<T> Fail(T data, IEnumerable<Notification> notifications) =>
            new Result<T>(false, data, notifications.ToArray());

        private Result(bool isSuccess, T data, params Notification[] errorMessages)
        {
            var doNotExistsErrorMessage = !errorMessages.Any();
            var doExistsErrorMessage = !doNotExistsErrorMessage;

            if (isSuccess)
            {
                if (doExistsErrorMessage)
                    throw new ArgumentException(
                        InternalErrorMessages.ErrorObjectIsProvidedForSuccess,
                        nameof(errorMessages));
            }
            else
            {
                if (doNotExistsErrorMessage)
                    throw new ArgumentNullException(
                        nameof(errorMessages),
                        InternalErrorMessages.ErrorObjectIsNotProvidedForFailure);
            }

            Data = data;
            Success = isSuccess;
            ErrorMessages = errorMessages;
        }
    }
}
