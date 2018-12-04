using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using APIUsuario.Models;
using System;
using System.Web.Http;

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


        [HttpGet]
        public ActionResult<List<Usuario>> GetAll()
        {
            //TODO: Desirable to serialize information about pagination and total of items
            //_context.Usuario.Count();
            return _context.Usuario.ToList();
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
            usuario.ds_login = item.ds_login;
            usuario.ds_email = item.ds_email;
            usuario.st_ativo = item.st_ativo;
            usuario.st_troca_senha = item.st_troca_senha;
            
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