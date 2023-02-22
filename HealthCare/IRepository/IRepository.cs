using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HealthCare.IRepository
{
    public interface IRepository
    {
        //Admin
        public interface IARepository<T> where T : class
        {
            Task<ActionResult<T>> GetById(int id);
            Task<ActionResult<IEnumerable<T>>> GetAll();
            Task<IActionResult> Create(T patient);
            Task<T> Update(int id, T admin);
            Task<T> Delete(int id);
        }
        //Doctor
        public interface IRepositoryDoctor<T> where T : class
        {
            Task<ActionResult<T>> GetById(int id);
            Task<ActionResult<IEnumerable<T>>> GetAll();
            Task<IActionResult> Create(T patient);
            Task<T> Update(int id, T admin);
            Task<T> Delete(int id);
        }
        //Patients
        public interface IPRepository<T> where T : class
        {
            Task<ActionResult<T>> GetById(int id);
            Task<ActionResult<IEnumerable<T>>> GetAll();
            Task<IActionResult> Create(T patient);
            Task<T> Update(int Patiid, T patient);
            Task<T> Delete(int id);
            Task<ActionResult<IEnumerable<T>>> SearchByName(string name);

        }
    }
}
