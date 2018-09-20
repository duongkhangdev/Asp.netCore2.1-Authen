using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DuongKhangDEV.Application.ViewModels.SystemCatalog;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using DuongKhangDEV.Application.Interfaces.SystemCatalog;
using System.Linq.Expressions;
using DuongKhangDEV.Data.Entities.SystemCatalog;

namespace DuongKhangDEV.WebApp.Areas.Admin.Controllers
{
    public class FunctionController : BaseController
    {
        #region Initialize

        private IFunctionService _functionService;
        
        public FunctionController(IFunctionService functionService)
        {
            this._functionService = functionService;
        }

        #endregion Initialize

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFilterAsync(string filter)
        {
            var model = await _functionService.GetAllFilterAsync(filter);
            return new ObjectResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var model = await _functionService.GetAllFilterAsync(string.Empty);
            var rootFunctions = model.Where(c => c.ParentId == null);
            var items = new List<FunctionViewModel>();
            foreach (var function in rootFunctions)
            {
                //add the parent category to the item list
                items.Add(function);
                //now get all its children (separate Category in case you need recursion)
                GetByParentId(model.ToList(), function, items);
            }
            return new ObjectResult(items);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var model = await _functionService.GetByIdAsync(id);

            return new ObjectResult(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPagingAsync(string keyword, int page, int pageSize)
        {            
            if (!String.IsNullOrEmpty(keyword))
            {
                Expression<Func<Function, bool>> myExpression;
                myExpression = i => i.Name.Contains(keyword);

                var model = await _functionService.GetAllPagingAsync(myExpression, page, pageSize);
                return new OkObjectResult(model);
            }
            else
            {
                var model = await _functionService.GetAllPagingAsync(page, pageSize);
                return new OkObjectResult(model);
            }            
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntityAsync(FunctionViewModel functionVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {
                if (functionVm.Id == "")
                {
                    await _functionService.InsertAsync(functionVm);
                }
                else
                {
                   await _functionService.UpdateAsync(functionVm);
                }
                
                return new OkObjectResult(functionVm);
            }
        }

        [HttpPost]
        public IActionResult UpdateParentId(string sourceId, string targetId, Dictionary<string, string> items)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _functionService.UpdateParentId(sourceId, targetId, items);
                    
                    return new OkResult();
                }
            }
        }

        [HttpPost]
        public IActionResult ReOrder(string sourceId, string targetId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if (sourceId == targetId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _functionService.ReOrder(sourceId, targetId);
                    
                    return new OkObjectResult(sourceId);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestResult();
            }
            else
            {
                await _functionService.DeleteAsync(id);
               
                return new OkObjectResult(id);
            }
        }

        #region Private Functions

        private void GetByParentId(IEnumerable<FunctionViewModel> allFunctions,
            FunctionViewModel parent, IList<FunctionViewModel> items)
        {
            var functionsEntities = allFunctions as FunctionViewModel[] ?? allFunctions.ToArray();
            var subFunctions = functionsEntities.Where(c => c.ParentId == parent.Id);
            foreach (var cat in subFunctions)
            {
                //add this category
                items.Add(cat);
                //recursive call in case your have a hierarchy more than 1 level deep
                GetByParentId(functionsEntities, cat, items);
            }
        }

        #endregion
    }
}