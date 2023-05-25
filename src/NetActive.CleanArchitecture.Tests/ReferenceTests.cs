namespace NetActive.CleanArchitecture.Tests
{
    using NetArchTest.Rules;

    [TestClass]
    public class ReferenceTests
    {
        private const string AutofacNamespace = "NetActive.CleanArchitecture.Autofac";

        private const string DomainNamespace = "NetActive.CleanArchitecture.Domain";
        private const string DomainFluentValidationNamespace = "NetActive.CleanArchitecture.DomainFluentValidation";
        
        private const string ApplicationNamespace = "NetActive.CleanArchitecture.Application";
        private const string ApplicationEntityFrameworkCoreNamespace = "NetActive.CleanArchitecture.Application.EntityFrameworkCore";
        private const string ApplicationMediatRNamespace = "NetActive.CleanArchitecture.Application.MediatR";
        private const string ApplicationPersistanceInterfacesNamespace = "NetActive.CleanArchitecture.Persistance.Interfaces";

        private const string PersistenceNamespace = "NetActive.CleanArchitecture.Persistence";
        private const string PersistenceEntityFrameworkCoreNamespace = "NetActive.CleanArchitecture.Persistence.EntityFrameworkCore";

        [TestMethod]
        public void Autofac_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Autofac.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                //AutofacNamespace,
                DomainNamespace,
                DomainFluentValidationNamespace,
                ApplicationNamespace,
                ApplicationEntityFrameworkCoreNamespace,
                ApplicationMediatRNamespace,
                ApplicationPersistanceInterfacesNamespace,
                PersistenceNamespace,
                PersistenceEntityFrameworkCoreNamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            Assert.IsTrue(testResult.IsSuccessful);
        }

        [TestMethod]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Domain.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                AutofacNamespace,
                //DomainNamespace,
                DomainFluentValidationNamespace,
                ApplicationNamespace,
                ApplicationEntityFrameworkCoreNamespace,
                ApplicationMediatRNamespace,
                ApplicationPersistanceInterfacesNamespace,
                PersistenceNamespace,
                PersistenceEntityFrameworkCoreNamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            Assert.IsTrue(testResult.IsSuccessful);
        }

        [TestMethod]
        public void DomainFluentValidationNamespace_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Domain.FluentValidation.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                AutofacNamespace,
                //DomainNamespace,
                //DomainFluentValidationNamespace,
                ApplicationNamespace,
                ApplicationEntityFrameworkCoreNamespace,
                ApplicationMediatRNamespace,
                ApplicationPersistanceInterfacesNamespace,
                PersistenceNamespace,
                PersistenceEntityFrameworkCoreNamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            Assert.IsTrue(testResult.IsSuccessful);
        }

        [TestMethod]
        public void Persistance_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Persistence.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                AutofacNamespace,
                //DomainNamespace,
                DomainFluentValidationNamespace,
                //ApplicationNamespace,
                ApplicationEntityFrameworkCoreNamespace,
                ApplicationMediatRNamespace,
                //ApplicationPersistanceInterfacesNamespace,
                //PersistenceNamespace,
                PersistenceEntityFrameworkCoreNamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            Assert.IsTrue(testResult.IsSuccessful);
        }

        [TestMethod]
        public void PersistanceEntityFrameworkCore_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Persistence.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                AutofacNamespace,
                //DomainNamespace,
                DomainFluentValidationNamespace,
                //ApplicationNamespace,
                ApplicationEntityFrameworkCoreNamespace,
                ApplicationMediatRNamespace,
                ApplicationPersistanceInterfacesNamespace,
                //PersistenceNamespace,
                //PersistenceEntityFrameworkCoreNamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            Assert.IsTrue(testResult.IsSuccessful);
        }

        [TestMethod]
        public void Application_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                AutofacNamespace,
                //DomainNamespace,
                DomainFluentValidationNamespace,
                //ApplicationNamespace,
                ApplicationEntityFrameworkCoreNamespace,
                ApplicationMediatRNamespace,
                ApplicationPersistanceInterfacesNamespace,
                PersistenceNamespace,
                PersistenceEntityFrameworkCoreNamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            Assert.IsTrue(testResult.IsSuccessful);
        }

        [TestMethod]
        public void ApplicationEntityFrameworkCore_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Application.EntityFrameworkCore.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                AutofacNamespace,
                //DomainNamespace,
                DomainFluentValidationNamespace,
                //ApplicationNamespace,
                //ApplicationEntityFrameworkCoreNamespace,
                ApplicationMediatRNamespace,
                //ApplicationPersistanceInterfacesNamespace,
                PersistenceNamespace,
                PersistenceEntityFrameworkCoreNamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            Assert.IsTrue(testResult.IsSuccessful);
        }

        [TestMethod]
        public void ApplicationMediatR_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Application.MediatR.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                AutofacNamespace,
                DomainNamespace,
                DomainFluentValidationNamespace,
                //ApplicationNamespace,
                ApplicationEntityFrameworkCoreNamespace,
                //ApplicationMediatRNamespace,
                ApplicationPersistanceInterfacesNamespace,
                PersistenceNamespace,
                PersistenceEntityFrameworkCoreNamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            Assert.IsTrue(testResult.IsSuccessful);
        }

        [TestMethod]
        public void ApplicationPersistenceInterfaces_Should_Not_HaveDependencyOnOtherProjects()
        {
            // Arrange
            var assembly = typeof(Application.Persistence.Interfaces.AssemblyReference).Assembly;

            var otherProjects = new[]
            {
                AutofacNamespace,
                DomainNamespace,
                DomainFluentValidationNamespace,
                //ApplicationNamespace,
                ApplicationEntityFrameworkCoreNamespace,
                ApplicationMediatRNamespace,
                //ApplicationPersistanceInterfacesNamespace,
                PersistenceNamespace,
                PersistenceEntityFrameworkCoreNamespace
            };

            // Act
            var testResult = Types
                .InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOnAny(otherProjects)
                .GetResult();

            // Assert
            Assert.IsTrue(testResult.IsSuccessful);
        }
    }
}