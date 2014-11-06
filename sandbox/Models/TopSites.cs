using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace sandbox.Models
{
    public class TopSites : IEnumerable
    {
        private List<TopSite> _topsites;
       
        public void Add(TopSite tp)
        {
            _topsites.Add(tp);
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return _topsites.ToList().GetEnumerator();
        }
    }
}
