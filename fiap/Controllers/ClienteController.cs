using fiap.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fiap.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Add()
        {
            return PartialView();
        }
        public ActionResult Detail()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult GetAll(int skip, int take)
        {
            var response = new JsonResponse();
            try
            {
                response.Data = biz.Cliente.GetAll(skip, take);
                response.Status = JsonResponseStatus.Êxito;
            }
            catch (Exception ex)
            {
                response.Status = JsonResponseStatus.Falha;
                response.Message = ex.Message; response.Exception = ex;
            }

            var json = JsonConvert.SerializeObject(response, Helper.Json.SerializerSettings);
            return new ContentResult { Content = json, ContentType = "application/json" };
        }
        public ActionResult Edit()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Get(string codigocliente)
        {
            var response = new JsonResponse();
            try
            {
                response.Data = biz.Cliente.Get(codigocliente);
                response.Status = JsonResponseStatus.Êxito;
            }
            catch (Exception ex)
            {
                response.Status = JsonResponseStatus.Falha;
                response.Message = ex.Message; response.Exception = ex;
            }

            var json = JsonConvert.SerializeObject(response, Helper.Json.SerializerSettings);
            return new ContentResult { Content = json, ContentType = "application/json" };
        }

        [HttpPost]
        public ActionResult Update(entities.Cliente item)
        {
            var response = new JsonResponse();
            try
            {
                response.Data = biz.Cliente.Update(item);
                response.Status = JsonResponseStatus.Êxito;
            }
            catch (Exception ex)
            {
                response.Status = JsonResponseStatus.Falha;
                response.Message = ex.Message; response.Exception = ex;
            }


            var json = JsonConvert.SerializeObject(response, Helper.Json.SerializerSettings);
            return new ContentResult { Content = json, ContentType = "application/json" };
        }

        [HttpPost]
        public ActionResult ValidaCpf(string cpf)
        {
            var response = new JsonResponse();
            try
            {
                var result = biz.Cliente.CheckCPF(cpf);
                response.Data = (result ? true : false);
                response.Status = JsonResponseStatus.Êxito;
            }
            catch (Exception ex)
            {
                response.Status = JsonResponseStatus.Falha;
                response.Message = ex.Message; response.Exception = ex;
            }

            var json = JsonConvert.SerializeObject(response, Helper.Json.SerializerSettings);
            return new ContentResult { Content = json, ContentType = "application/json" };

        }

        [HttpPost]
        public ActionResult Save(entities.Cliente item)
        {
            var response = new JsonResponse();
            try
            {
                response.Data = biz.Cliente.Save(item);
                response.Status = JsonResponseStatus.Êxito;
            }
            catch (Exception ex)
            {
                response.Status = JsonResponseStatus.Falha;
                response.Message = ex.Message; response.Exception = ex;
            }

            var json = JsonConvert.SerializeObject(response, Helper.Json.SerializerSettings);
            return new ContentResult { Content = json, ContentType = "application/json" };
        }
        [HttpPost]
        public ActionResult Delete(string codigo_cliente)
        {
            var response = new JsonResponse();
            try
            {
                response.Data = biz.Cliente.Delete(codigo_cliente);
                response.Status = JsonResponseStatus.Êxito;
            }
            catch (Exception ex)
            {
                response.Status = JsonResponseStatus.Falha;
                response.Message = ex.Message; response.Exception = ex;
            }


            var json = JsonConvert.SerializeObject(response, Helper.Json.SerializerSettings);
            return new ContentResult { Content = json, ContentType = "application/json" };
        }
    }
}
