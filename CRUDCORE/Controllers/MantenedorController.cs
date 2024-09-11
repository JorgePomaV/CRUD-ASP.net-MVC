using CRUDCORE.Datos;
using Microsoft.AspNetCore.Mvc;
using CRUDCORE.Models;



namespace CRUDCORE.Controllers
{
    public class MantenedorController : Controller
    {
        ContactoDatos contactoDatos = new ContactoDatos();
        public IActionResult Listar()
        {
            var oLista = contactoDatos.ListarContactos();
            return View(oLista);
        }

        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(ContactoModel contactoModel)
        {
            if (!ModelState.IsValid)//ModelState.IsValid verifica si el contactoModel esta vacio: true=cuando este todo vien false= cuando este el campo vacio
            {
                return View();//si el campo esta vacio retorna la vista Guardar
            }

             var resultado = contactoDatos.setGuardar(contactoModel);
             if (resultado)
             {
                 //retorna el metodo Listar si el resultado es true
                 return RedirectToAction("Listar");
             }
             else
             {
                 return View();
             }
        }

        public IActionResult Editar(int IdContacto)
        {
            var contacto = contactoDatos.GetContacto(IdContacto);
            return View(contacto);
        }

        [HttpPost]
        public IActionResult Editar(ContactoModel contactoModel)
        {

            if (!ModelState.IsValid)//ModelState.IsValid verifica si el contactoModel esta vacio: true=cuando este todo vien false= cuando este el campo vacio
            {
                return View();//si el campo esta vacio retorna la vista Guardar
            }

            var resultado = contactoDatos.setEditar(contactoModel);
            if (resultado)
            {
                //retorna el metodo Listar si el resultado es true
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }


        public IActionResult Eliminar(int IdContacto)
        {
            var contacto = contactoDatos.GetContacto(IdContacto);
            return View(contacto);
        }

        [HttpPost]
        public IActionResult Eliminar(ContactoModel contactoModel)
        {

            var resultado = contactoDatos.Eliminar(contactoModel.IdContacto);
            if (resultado)
            {
                //retorna el metodo Listar si el resultado es true
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }
    }
}
