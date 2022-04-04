using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Commands;
using Todo.Domain.Handlers;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests
{
    [TestClass]
    public class CreateTodoHandlerTests
    {
        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", "", DateTime.Now);
        private readonly CreateTodoCommand _validCommand =
            new CreateTodoCommand("Ir ao shopping", "tiagoBarbosa", DateTime.Now.AddDays(2));
        private readonly TodoHandler _handler = new TodoHandler(new FakeTodoRepository());
        
        [TestMethod]
        public void Dado_um_comando_invalido_deve_interromper_a_execucao()
        {
            var result = (GenericCommandResult)_handler.Handle(_invalidCommand);
            Assert.AreEqual(false, result.Success);
        }
        
        [TestMethod]
        public void Dado_um_comando_valido_deve_interromper_a_execucao()
        {
            var result = (GenericCommandResult)_handler.Handle(_validCommand);
            Assert.AreEqual(true, result.Success);
        }
    }
}