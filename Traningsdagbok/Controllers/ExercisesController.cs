using Microsoft.AspNetCore.Mvc;
using Traningsdagbok.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Traningsdagbok.Controllers
{
    public class ExercisesController : Controller
    {

      
        public async Task<IActionResult> Index()
        {
            Database db = new Database();
            var exercises = await db.GetExerciseAsync();
            return View(exercises);
        }

        public async Task<IActionResult> Create()
        {
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Exercise exercise)
        {
            
            Database db = new Database();
            await db.SaveExercise(exercise);
            return RedirectToAction("Index", "ExerciseSession");
        }

        public async Task<IActionResult> Edit(string id)
        {
            Database db = new Database();
            var exercise = await db.GetExercise(id);

            return View(exercise);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Exercise exercise)
        {
            Database db = new Database();
            exercise.Id = new ObjectId(id); 
            await db.UpdateExercise(exercise);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(string id)
        {
            Database db = new Database();
            var exercise = await db.GetExercise(id);

            return View(exercise);
        }

        public async Task<IActionResult> Delete(string id)
        {
            Database db = new Database();
            await db.DeleteExercise(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExercise(string id)
        {
            Database db = new Database();
            await db.DeleteExercise(id);
            return Redirect("/Exercises");
        }

    }
}
