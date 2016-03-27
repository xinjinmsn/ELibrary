using ELibrary.Data;
using ELibrary.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ELibrary.WebAPI.Controllers
{

    public abstract class BaseApiController : ApiController
    {
        private ModelFactory _modelFactory;
        private IELibraryRepository _repo;
        public BaseApiController(IELibraryRepository repo)
        {
            _repo = repo;
            
        }

        protected IELibraryRepository TheRepository
        {
            get
            {
                return _repo;
            }
        }

        //Defer the creation of the model factory
        protected ModelFactory TheModelFactory
        {
            get
            {
                if(_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(this.Request);
                }

                return _modelFactory;
            }
        }
    }
}
