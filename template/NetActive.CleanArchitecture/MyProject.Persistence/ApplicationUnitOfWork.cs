﻿namespace MyProject.Persistence
{
	using MyProject.Application.Interfaces.Persistence;
	using NetActive.CleanArchitecture.Persistence.EntityFrameworkCore;

	public class ApplicationUnitOfWork : EfUnitOfWork, IApplicationUnitOfWork
	{
		public ApplicationUnitOfWork(ApplicationDbContext context) : base(context)
		{
		}
	}
}
