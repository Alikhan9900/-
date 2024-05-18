using System.Collections.Generic;
using System.Text;

class LightElementNode : LightNode
{
    public string TagName { get; set; }
    public bool IsBlock { get; set; }
    public bool IsSelfClosing { get; set; }
    public List<string> CssClasses { get; set; }
    public List<LightNode> Children { get; set; }

    public LightElementNode(string tagName, bool isBlock = true, bool isSelfClosing = false)
    {
        TagName = tagName;
        IsBlock = isBlock;
        IsSelfClosing = isSelfClosing;
        CssClasses = new List<string>();
        Children = new List<LightNode>();
    }

    public void AddChild(LightNode child)
    {
        if (!IsSelfClosing)
        {
            Children.Add(child);
        }
    }

    public override string OuterHTML
    {
        get
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"<{TagName}");

            if (CssClasses.Count > 0)
            {
                sb.Append($" class=\"{string.Join(" ", CssClasses)}\"");
            }

            if (IsSelfClosing)
            {
                sb.Append(" />");
            }
            else
            {
                sb.Append(">");
                foreach (var child in Children)
                {
                    sb.Append(child.OuterHTML);
                }
                sb.Append($"</{TagName}>");
            }

            return sb.ToString();
        }
    }

    public override string InnerHTML
    {
        get
        {
            if (IsSelfClosing)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            foreach (var child in Children)
            {
                sb.Append(child.InnerHTML);
            }
            return sb.ToString();
        }
    }
}
