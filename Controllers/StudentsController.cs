using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StudentAdoNet.Data;
using StudentAdoNet.Models;

namespace StudentAdoNet.Controllers;

public class StudentsController : Controller
{
    private readonly StudentRepository _repository;

    public StudentsController(StudentRepository repository)
    {
        _repository = repository;
    }


    public async Task<IActionResult> Index()
    {
        var students = await _repository.GetAllAsync();
        return View(students);
    }


    public async Task<IActionResult> Details(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }


    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {

        if (!ModelState.IsValid)
        {
            return View(student);
        }


        if (await _repository.EmailExistsAsync(student.Email))
        {
            ModelState.AddModelError(nameof(Student.Email), "This email address is already registered.");
            return View(student);
        }


        try
        {
            await _repository.AddAsync(student);
        }
        catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
        {
            ModelState.AddModelError(nameof(Student.Email), "This email address is already registered.");
            return View(student);
        }


        TempData["SuccessMessage"] = $"Student {student.FullName} registered successfully.";
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Edit(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(student);
        }


        if (await _repository.EmailExistsAsync(student.Email, excludeId: id))
        {
            ModelState.AddModelError(nameof(Student.Email), "This email address is already registered.");
            return View(student);
        }

        try
        {
            var updated = await _repository.UpdateAsync(student);
            if (!updated)
            {
                return NotFound();
            }
        }
        catch (SqlException ex) when (ex.Number == 2601 || ex.Number == 2627)
        {
            ModelState.AddModelError(nameof(Student.Email), "This email address is already registered.");
            return View(student);
        }

        TempData["SuccessMessage"] = $"Student {student.FullName} updated successfully.";
        return RedirectToAction(nameof(Index));
    }


    public async Task<IActionResult> Delete(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }


    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Student deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
