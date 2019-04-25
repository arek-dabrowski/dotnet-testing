using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPMVC.TagHelpers
{
    [HtmlTargetElement("gun-details")]
    public class GunDetailTagHelper : TagHelper
    {
        [HtmlAttributeName("for-name")]
        public ModelExpression GunName { get; set; }
        [HtmlAttributeName("for-production")]
        public ModelExpression ProductionDate { get; set; }
        [HtmlAttributeName("for-type")]
        public ModelExpression Type { get; set; }
        [HtmlAttributeName("for-caliber")]
        public ModelExpression Caliber { get; set; }
        [HtmlAttributeName("for-price")]
        public ModelExpression Price { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "GunDetails";
            output.TagMode = TagMode.StartTagAndEndTag;

            var sb = new StringBuilder();
            sb.AppendFormat("<td>{0}</td>", this.GunName.Model);
            sb.AppendFormat("<td>{0}</td>", this.ProductionDate.Model);
            sb.AppendFormat("<td>{0}</td>", this.Type.Model);
            sb.AppendFormat("<td>{0}</td>", this.Caliber.Model);
            sb.AppendFormat("<td>{0}</td>", this.Price.Model);

            output.PreContent.SetHtmlContent(sb.ToString());
        }
    }
}
