﻿
using System.Linq;

using Employee.DataAccess.Abstractions;
using Employee.DataAccess.Repositories;
using Employee.TestData;

using FluentAssertions;

using Xunit;

namespace Employee.DataAccess.Tests
{
    [Collection("EmployeeRepository")]
    public class EmployeeRepositoryTests
    {
        [Fact]
        public void ShouldReturnEmptyGetAll()
        {
            // Arrange
            using (IEmployeeContext employeeContext = new EmployeeContext())
            {
                IEmployeeRepository employeeRepository = new EmployeeRepository(employeeContext);

                // Act
                var allEmployees = employeeRepository.GetAll().ToList();

                 // Assert
                allEmployees.Should().HaveCount(0);
            }
        }

        [Fact]
        public void ShouldAddEmployee()
        {
            // Arrange
            using (IEmployeeContext employeeContext = new EmployeeContext())
            {
                IEmployeeRepository employeeRepository = new EmployeeRepository(employeeContext);
                var employee = CreateEntity.Employee1;

                // Act
                employeeRepository.Add(employee);
                var numberOfChangesCommitted = employeeContext.SaveChanges();

                // Assert
                numberOfChangesCommitted.Should().Be(1);

                var allEmployees = employeeRepository.GetAll().ToList();
                allEmployees.Should().HaveCount(1);
                allEmployees.ElementAt(0).ShouldBeEquivalentTo(employee, options => options.IncludingAllDeclaredProperties());
            }
        }

        [Fact]
        public void ShouldDeleteEmployee()
        {
            // Arrange
            using (IEmployeeContext employeeContext = new EmployeeContext())
            {
                IEmployeeRepository employeeRepository = new EmployeeRepository(employeeContext);
                var employee1 = CreateEntity.Employee1;
                var employee2 = CreateEntity.Employee2;

                employeeRepository.Add(employee1);
                employeeRepository.Add(employee2);
                var numberOfAdds = +employeeContext.SaveChanges();

                // Act
                employeeRepository.Delete(employee2);
                var numberOfDeletes =+ employeeContext.SaveChanges();

                // Assert
                numberOfAdds.Should().Be(2);
                numberOfDeletes.Should().Be(1);

                var allEmployees = employeeRepository.GetAll().ToList();
                allEmployees.Should().HaveCount(1);
                allEmployees.ElementAt(0).ShouldBeEquivalentTo(employee1, options => options.IncludingAllDeclaredProperties());
            }
        }
    }
}
