using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Poc.DotNetCore.Swagger.ViewModels;

namespace Poc.DotNetCore.Swagger.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Obter todos os usuários.        
        /// </summary> 
        /// <remarks>
        /// Exemplos de retorno:
        /// 
        ///     Status Code: 200
        /// 
        ///     [
        ///         {
        ///             "id": "caef4812-598e-4473-8efc-b338af69a18f",
        ///             "name": "João Pedro Hudinik",
        ///             "email": "hudinik@outlook.com"
        ///         },
        ///         {
        ///             "id": "b88dccaf-a3a2-436f-8b09-27f0e1f73321",
        ///             "name": "José",
        ///             "email": "jose@outlook.com"
        ///         }        
        ///     ]  
        /// 
        ///     Status Code: 500
        ///     
        ///     null
        /// 
        /// </remarks>       
        /// <response code="200">A lista de usuários foi obtida com sucesso.</response>
        /// <response code="500">Ocorreu um erro ao obter a lista de usuários.</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult<List<User>> Get()
        {
            try
            {
                var users = GetUsers();

                HttpContext.Response.StatusCode = 200;

                return users;
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = 500;
                return null;
            }
        }

        /// <summary>
        /// Obter um usuário específico por ID.
        /// </summary>
        /// <remarks>
        /// Exemplos de retorno:
        /// 
        ///     Status Code: 200
        ///         
        ///     {
        ///         "id": "caef4812-598e-4473-8efc-b338af69a18f",
        ///         "name": "João Pedro Hudinik",
        ///         "email": "hudinik@outlook.com"
        ///     }        
        /// 
        ///     Os demais status retornam nulo.
        ///     
        /// </remarks>       
        /// <param name="id">ID do usuário.</param>        
        /// <response code="200">O usuário foi obtido com sucesso.</response>                
        /// <response code="404">Não foi encontrado usuário com ID especificado.</response>
        /// <response code="500">Ocorreu um erro ao obter o usuário.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult<User> Get(Guid id)
        {
            try
            {
                var users = GetUsers();

                var user = users.FirstOrDefault(x => x.Id.Equals(id));

                if (user == null)
                {
                    HttpContext.Response.StatusCode = 404;
                    return null;
                }

                HttpContext.Response.StatusCode = 200;
                return user;
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = 500;
                return null;
            }
        }

        /// <summary>
        /// Cadastrar usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///       
        ///     {        
        ///        "Name": "João",
        ///        "Email": "hudinik@outlook.com"
        ///     }                
        ///     
        /// </remarks>
        /// <param name="user">Modelo do usuário.</param>
        /// <response code="200">O usuário foi cadastrado com sucesso.</response>        
        /// <response code="400">O modelo do usuário enviado é inválido.</response>
        /// <response code="500">Ocorreu um erro ao cadastrar o usuário.</response>
        [HttpPost]
        public void Post([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Response.StatusCode = 400;
                    return;
                }

                HttpContext.Response.StatusCode = 200;
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = 500;
            }
        }

        /// <summary>
        /// Alterar usuário.
        /// </summary> 
        /// <remarks>
        /// Exemplo de request:
        ///       
        ///     {  
        ///        "Id": "CAEF4812-598E-4473-8EFC-B338AF69A18F",
        ///        "Name": "João",
        ///        "Email": "hudinik@outlook.com"
        ///     }
        ///        
        /// </remarks>        
        /// <param name="user">Modelo do usuário.</param>
        /// <response code="200">O usuário foi alterado com sucesso.</response>        
        /// <response code="400">O modelo do usuário enviado é inválido.</response>
        /// <response code="404">Não foi encontrado usuário com ID especificado.</response>
        /// <response code="500">Ocorreu um erro ao alterar o usuário.</response>
        [HttpPut("{id}")]
        public void Put([FromBody] User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    HttpContext.Response.StatusCode = 400;
                    return;
                }

                var users = GetUsers();

                var dbUser = users.FirstOrDefault(x => x.Id.Equals(user.Id));

                if (dbUser == null)
                {
                    HttpContext.Response.StatusCode = 404;
                    return;
                }

                HttpContext.Response.StatusCode = 200;
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = 500;
            }
        }

        /// <summary>
        /// Deletar usuário.
        /// </summary>   
        /// <param name="id">ID do usuário.</param>
        /// <response code="200">O usuário foi deletado com sucesso.</response>                
        /// <response code="404">Não foi encontrado usuário com ID especificado.</response>
        /// <response code="500">Ocorreu um erro ao deletar o usuário.</response>
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            try
            {               
                var users = GetUsers();

                var dbUser = users.FirstOrDefault(x => x.Id.Equals(id));

                if (dbUser == null)
                {
                    HttpContext.Response.StatusCode = 404;
                    return;
                }

                HttpContext.Response.StatusCode = 200;
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = 500;
            }
        }

        private List<User> GetUsers()
        {
            return new List<User>
            {
                new User { Id = Guid.Parse("CAEF4812-598E-4473-8EFC-B338AF69A18F"), Name = "João Pedro Hudinik", Email = "hudinik@outlook.com" },
                new User { Id = Guid.Parse("B88DCCAF-A3A2-436F-8B09-27F0E1F73321"), Name = "José", Email = "jose@outlook.com" },
                new User { Id = Guid.Parse("1E018A4E-FC1B-4E2D-91CA-8076D60CF63F"), Name = "Maria", Email = "maria@outlook.com" }
            };
        }
    }
}
