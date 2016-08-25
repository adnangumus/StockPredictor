using StockPredictor.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPredictor.Tests
{
    class YahooMinerTest
    {
        public void testLinkProcessor()
        {
            YahooMiner ym = new YahooMiner();
            ym.getYahoolinks("https://news.search.yahoo.com/search;_ylt=AwrXgiOkV75Xm1gAkqnQtDMD;_ylu=X3oDMTB0ZWYwNXBqBGNvbG8DZ3ExBHBvcwMxBHZ0aWQDBHNlYwNzb3J0?p=gild&type=pivot_us_srp_yahoonews&ei=UTF-8&flt=ranking%3Adate%3B&fr=yfp-t");
        }
    }
}

//http://www.wsj.com/articles/mylan-faces-scrutiny-over-epipen-price-increases-1472074823?ru=yahoo?mod=yahoo_itp&yptr=yahoo/RK=0/RS=uiRiQJjePgbRGPMLbNS5VoKy46s-
//http://r.search.yahoo.com/_ylt=AwrXoCFFgL5XVEAAmbnQtDMD;_ylu=X3oDMTByYnR1Zmd1BGNvbG8DZ3ExBHBvcwMyBHZ0aWQDBHNlYwNzcg--/RV=2/RE=1472131269/RO=10/RU=http%3a%2f%2fseekingalpha.com%2fnews%2f3205187-candidate-clintons-harsh-comments-high-price-mylans-epipen-sends-biotechs-reeling-ibb-2%3fsource%3dfeed_news_all/RK=0/RS=zFgN4R7aWA1iiBCk_mCDrWjzA54-
//  http://r.search.yahoo.com/_ylt=AwrXnCf5jb5XWXAA.bLQtDMD;_ylu=X3oDMTByc3RzMXFjBGNvbG8DZ3ExBHBvcwM0BHZ0aWQDBHNlYwNzcg--/RV=2/RE=1472134778/RO=10/RU=https%3a%2f%2fwww.thestreet.com%2fstory%2f13684156%2f1%2fgilead-sciences-gild-stock-price-target-cut-leerink-weak-hiv-sales.html%3fpuc%3dyahoo%26cm_ven%3dYAHOO%26yptr%3dyahoo/RK=0/RS=zXtPLg1xyl5CZ6kfALH.buAMWgQ-
//     http://r.search.yahoo.com/_ylt=AwrXnCf5jb5XWXAA.7LQtDMD;_ylu=X3oDMTByNDZ0aWFxBGNvbG8DZ3ExBHBvcwM2BHZ0aWQDBHNlYwNzcg--/RV=2/RE=1472134778/RO=10/RU=http%3a%2f%2fseekingalpha.com%2farticle%2f4001799-teslas-new-car-can-0minus-60-2_5-seconds-stock-will-take-2_5-years/RK=0/RS=Ggf_k_UkLK0_HEy1CSjzG4MD9js-
//http%3a%2f%2fseekingalpha.com%2fnews%2f3205187-candidate-clintons-harsh-comments-high-price-mylans-epipen-sends-biotechs-reeling-ibb-2%3fsource%3dfeed_news_all/RK=0/RS=zFgN4R7aWA1iiBCk_mCDrWjzA54-
//https://news.search.yahoo.com/search;_ylt=AwrXgiOkV75Xm1gAkqnQtDMD;_ylu=X3oDMTB0ZWYwNXBqBGNvbG8DZ3ExBHBvcwMxBHZ0aWQDBHNlYwNzb3J0?p=gild&type=pivot_us_srp_yahoonews&ei=UTF-8&flt=ranking%3Adate%3B&fr=yfp-t
//"https://news.search.yahoo.com/search;_ylt=AwrXgiOkV75Xm1gAkqnQtDMD;_ylu=X3oDMTB0ZWYwNXBqBGNvbG8DZ3ExBHBvcwMxBHZ0aWQDBHNlYwNzb3J0?p=" +  + "+" + + "&type=pivot_us_srp_yahoonews&ei=UTF-8&flt=ranking%3Adate%3B&fr=yfp-t
//https://news.search.yahoo.com/search;_ylt=AwrXgiOkV75Xm1gAkqnQtDMD;_ylu=X3oDMTB0ZWYwNXBqBGNvbG8DZ3ExBHBvcwMxBHZ0aWQDBHNlYwNzb3J0?p=gild&type=pivot_us_srp_yahoonews&ei=UTF-8&flt=ranking%3Adate%3B&fr=yfp-t

//http%3a%2f%2fseekingalpha.com%2fnews%2f3205187-candidate-clintons-harsh-comments-high-price-mylans-epipen-sends-biotechs-reeling-ibb-2%3fsource%3dfeed_news_all/RK=0/RS=zFgN4R7aWA1iiBCk_mCDrWjzA54-
//http://r.search.yahoo.com/_ylt=AwrXnCfKkL5XtVwAtg_QtDMD;_ylu=X3oDMTByMjR0MTVzBGNvbG8DZ3ExBHBvcwM3BHZ0aWQDBHNlYwNzcg--/RV=2/RE=1472135499/RO=10/RU=http%3a%2f%2fwww.fool.com%2finvesting%2f2016%2f08%2f24%2fwhy-you-should-and-shouldnt-buy-gilead-sciences-in.aspx%3fsource%3deptfxblnk0000004/RK=0/RS=bCiptg8.GTi7RklXzptqNVziy60-
//string decodedUrl = HttpUtility.UrlDecode(url)}
//http://seekingalpha.com/article/4001799-teslas-new-car-can-0minus-60-2_5-seconds-stock-will-take-2_5-years/RK=0/RS=MMVjt0Q6_p3Juxt8cXnIbwv8M2Q-


