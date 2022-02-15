namespace GSoulavy.Template.WindowsService.Kestrel.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
///     Request object
/// </summary>
public class Request
{
    /// <summary>
    ///     The name of the request
    /// </summary>
    [Required]
    public string? Name { get; set; }
}
