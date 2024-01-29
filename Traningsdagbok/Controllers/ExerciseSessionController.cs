using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Traningsdagbok.Models;

namespace Traningsdagbok.Controllers
{
    public class ExerciseSessionController : Controller
    {
        private readonly Database _database;

        public ExerciseSessionController(Database database)
        {
            _database = database;
        }

        public async Task<IActionResult> Index()
        {
           
            var exerciseSessions = await _database.GetExerciseSessionsAsync();

            
            foreach (var session in exerciseSessions)
            {
                
                session.Exercise = await _database.GetExercise(session.ExerciseId.ToString());
            }

           
            return View(exerciseSessions);
        }

        public async Task<IActionResult> Create()
        {
            var exerciseList = await _database.GetExerciseAsync();
            ViewBag.ExerciseList = new SelectList(exerciseList, "Id", "Name");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ExerciseSession exerciseSession)
        {
          
            exerciseSession.Date = exerciseSession.Date.Date;

           
            await _database.SaveExerciseSession(exerciseSession);

            
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Delete(string id)
        {
          
            await _database.DeleteExerciseSession(id);

            return RedirectToAction(nameof(Index));
        }



    }
}
