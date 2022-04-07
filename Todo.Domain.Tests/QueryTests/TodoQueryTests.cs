using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.QueryTests
{
    [TestClass]
    public class TodoQueryTests
    {
        private List<TodoItem> _items;

        public TodoQueryTests()
        {
            _items = new List<TodoItem>();
            _items.Add(new TodoItem("Task 01", "userA", DateTime.Now));
            _items.Add(new TodoItem("Task 02", "tiagobarbosa", DateTime.Now));
            _items.Add(new TodoItem("Task 03", "userA", DateTime.Now));
            _items.Add(new TodoItem("Task 04", "userA", DateTime.Now));
            _items.Add(new TodoItem("Task 05", "tiagobarbosa", DateTime.Now));
        }

        [TestMethod]
        public void Deve_retornar_tarefas_apensa_do_usuario_tiagobarbosa()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("tiagobarbosa"));
            Assert.AreEqual(2, result.Count());
        }
    }
}