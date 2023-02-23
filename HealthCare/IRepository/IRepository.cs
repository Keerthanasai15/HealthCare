using HealthCare.Models;
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

        public interface IAPRepository<T> where T : class
        {
            IEnumerable<T> GetAll();

            Task<T> GetById(int id);

            Task Create(T obj);

            Task<Appointment> Update(int id, T obj);

            Task<Appointment> Delete(int id);

        }

        public interface IRepositoryADR<T> where T : class
        {
            Task<ActionResult<T>> GetById(int id);
            Task<ActionResult<IEnumerable<T>>> GetAll();
            Task<IActionResult> Create(T customer);
            Task<T> Update(int id, T admin);
            Task<T> Delete(int id);
        }

        public interface IRepositoryStatus<T> where T : class
        {
            Task<ActionResult<T>> GetById(int id);
            Task<ActionResult<IEnumerable<T>>> GetAll();
            Task<IActionResult> Create(T customer);
            Task<T> Update(int id, T admin);
            Task<T> Delete(int id);

        }

       

    }
}
