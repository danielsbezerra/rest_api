using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using APIUsuario.Models;
using System;
using System.Web.Http;
using Newtonsoft.Json;


namespace APIUsuario.api.Controllers
{
    //Add versioning
    [ApiVersion( "0.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioContext _context;

        public UsuarioController(UsuarioContext context)
        {
            _context = context;
        }


        // [HttpGet]
        // public ActionResult<List<Usuario>> GetAll()
        // {
        //     //TODO: Desirable to serialize information about pagination and total of items
        //     //_context.Usuario.Count();
        //     return _context.Usuario.ToList();
        // }


        [HttpGet]
        public IEnumerable<Usuario> GetAll([FromQuery]PagingModel pagingmodel)
        {
            // Return List of Customer  
            var source = (from customer in _context.Usuario.  
                    OrderBy(a => a.id_usuario)  
                  select customer).AsQueryable();

            // Get # of rows
            int count = source.Count();
        
            // Parameter is passed from query string. If it is null, then default defined in the model (1)
            int CurrentPage = pagingmodel.pageNumber;
        
            // Parameter is passed from query string. If it is null, then default defined in the model (20)
            int ItemsPerPage = pagingmodel.itemsPerPage;
        
            // Display TotalCount to Records to User
            int TotalCount = count;
        
            // Calculating Totalpage by Dividing (# of Records / ItemsPerPage)
            int TotalPages = (int)Math.Ceiling(count / (double)ItemsPerPage);
        
            // Returns List of Usuarios after applying Paging
            var items = source.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
        
            // if CurrentPage is greater than 1 means it has previousPage
            string previousPageLink;
            if (CurrentPage > 1) {
                previousPageLink = "/url/previous";
            }
            else {
                previousPageLink = "";
            }


            // if TotalPages is greater than CurrentPage means it has nextPage
            string nextPageLink;
            if (CurrentPage < TotalPages) {
                nextPageLink = "/url/next";
            }
            else {
                nextPageLink = "";
            }


            // Object which we are going to send in header
            var paginationMetadata = new
            {  
                totalCount = TotalCount,
                itemsPerPage = ItemsPerPage,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPageLink,
                nextPageLink
            };
        
            // Setting Header
            HttpContext.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject (paginationMetadata));

            // Returing List of Customers Collections
            return items;

        }


        [HttpGet("{id:int}", Name = "GetUsuario")]
        public ActionResult<Usuario> GetById(int id)
        {
            var item = _context.Usuario.Find(id);
            if (item == null)
            {
                return NotFound();
            }       
            return item;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Usuario item)
        {

            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            _context.Usuario.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetUsuario", new { id = item.id_usuario }, item);
        }

        //To support partial updates, use HTTP PATCH.
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Usuario item)
        {
            if (!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.ds_nome = item.ds_nome;
            usuario.id_local_acesso = item.id_local_acesso;
            usuario.ds_login = item.ds_login;
            usuario.ds_email = item.ds_email;
            usuario.st_ativo = item.st_ativo;
            usuario.st_troca_senha = item.st_troca_senha;
            usuario.st_excluido = item.st_excluido;
            
            
            _context.Usuario.Update(usuario);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var usuario = _context.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            _context.SaveChanges();
            return NoContent();
        }

    }
}