using Microsoft.AspNetCore.Razor.TagHelpers;

namespace VendasLanches.TagHelpers;

public class EmailTagHelper : TagHelper {
    public string EMail { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;

    public override void Process(TagHelperContext context, TagHelperOutput output) {
        output.TagName = "a";
        output.Attributes.SetAttribute("href", "mailto:" + EMail);
        output.Content.SetContent(Content);
    }
}
