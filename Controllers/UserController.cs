using CRUD_application_2.Models;
using System.Linq;
using System.Web.Mvc;

namespace CRUD_application_2.Controllers
{
    public class UserController : Controller
    {
        public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();
        // GET: User
        public ActionResult Index()
        {
            // Implement the Index method here
            return View(userlist);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            // Implement the details method here
            User user = GetUserById(id); // Assume this method returns a user by id
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);

        }

        // GET: User/Create
        public ActionResult Create()
        {
            //Implement the Create method here
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            // Implement the Create method (POST) here
            if (ModelState.IsValid)
            {
                AddUser(user);

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // This method is responsible for displaying the view to edit an existing user with the specified ID.
            // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.

            User user = userlist.FirstOrDefault(u => u.Id == id);

            // If the user wasn't found, return a 404
            if (user == null)
            {
                return HttpNotFound();
            }

            // Pass the user to the view
            return View(user);

        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
            // It receives user input from the form submission and updates the corresponding user's information in the userlist.
            // If successful, it redirects to the Index action to display the updated list of users.
            // If no user is found with the provided ID, it returns a HttpNotFoundResult.
            // If an error occurs during the process, it returns the Edit view to display any validation errors.

            // Find the user in the list
            User existingUser = userlist.FirstOrDefault(u => u.Id == id);

            // If the user wasn't found, return a 404
            if (existingUser == null)
            {
                return HttpNotFound();
            }

            // If the model state is not valid, return the form with the current data and validation errors
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            // Update the user's information
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            // Continue updating other properties as needed

            // Redirect to the Index view
            return RedirectToAction("Index");

        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Implement the Delete method here

            // Find the user in the list
            User user = userlist.FirstOrDefault(u => u.Id == id);

            // If the user wasn't found, return a 404
            if (user == null)
            {
                return HttpNotFound();
            }

            // Pass the user to the view
            return View(user);

        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Implement the Delete method (POST) here

            // Find the user in the list
            User user = userlist.FirstOrDefault(u => u.Id == id);

            // If the user wasn't found, return a 404
            if (user == null)
            {
                return HttpNotFound();
            }

            // Remove the user from the list
            userlist.Remove(user);

            // Redirect to the Index view
            return RedirectToAction("Index");
        }

        private User GetUserById(int id)
        {
            return userlist.FirstOrDefault(u => u.Id == id);
        }

        private void AddUser(User user)
        {
            // You might want to check if the user already exists in the list based on some criteria (like ID or email)
            // If the user already exists, you can decide whether to throw an error, ignore, or update the existing user

            // For now, let's assume that the user doesn't exist in the list and simply add the user
            user.Id = userlist.Any() ? userlist.Max(u => u.Id) + 1 : 1;

            userlist.Add(user);
        }
        public ActionResult Search(string searchString)
        {
            var filteredUsers = string.IsNullOrEmpty(searchString)
                ? userlist
                : userlist.Where(u => u.Name.Contains(searchString) || u.Email.Contains(searchString));

            return View("Index", filteredUsers);
        }

    }
}




