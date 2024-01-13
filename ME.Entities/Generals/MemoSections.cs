using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UserControls.Enums
{
    public enum MemoSections
    {
        /// <summary>
        ///  میتواند یک نام، آدرس اینترنتی یا هر دوی این موارد باشد. URI یک
        ///   فقط آدرس اینترنتی است URL درحالی که یک
        ///   ها هستند URI ها زیر مجموعهURL 
        ///   https://virgool.io/@CodeFriend/%D8%AA%D9%81%D8%A7%D9%88%D8%AA-url-%D9%88-uri-ihfb2nqekupb#:~:text=%DB%8C%DA%A9%20URI%20%D9%85%DB%8C%D8%AA%D9%88%D8%A7%D9%86%D8%AF%20%DB%8C%DA%A9%20%D9%86%D8%A7%D9%85,%D9%87%D8%B1%20URI%20%DB%8C%DA%A9%20URL%20%D9%86%DB%8C%D8%B3%D8%AA.
        /// </summary>
        Uri,
        Username,
        DatabaseName
    }
}
