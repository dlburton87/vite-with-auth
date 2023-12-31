using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SampleReact.AuthServer.ViewModels.Authorization;

public class LogoutViewModel
{
    [BindNever]
    public string RequestId { get; set; } = string.Empty;
}