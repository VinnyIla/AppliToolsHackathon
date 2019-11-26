using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackathonProj.PageObjects
{
    class PageManager : BasePage
    {
        public PageManager(String title = "")
       : base(title)
        {
            PageFactory.InitElements(Driver, this);
        }


        public AllPage allPage = new AllPage("");
    }
}
