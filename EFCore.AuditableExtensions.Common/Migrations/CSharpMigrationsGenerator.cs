﻿using EFCore.AuditableExtensions.Common.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace EFCore.AuditableExtensions.Common.Migrations;

internal class CSharpMigrationsGenerator : Microsoft.EntityFrameworkCore.Migrations.Design.CSharpMigrationsGenerator
{
    public CSharpMigrationsGenerator(MigrationsCodeGeneratorDependencies dependencies, CSharpMigrationsGeneratorDependencies csharpDependencies) : base(dependencies, csharpDependencies)
    { }

    protected override IEnumerable<string> GetNamespaces(IEnumerable<MigrationOperation> operations) => base.GetNamespaces(operations).Concat(operations.OfType<IDependentMigrationOperation>().SelectMany(GetDependentNamespaces));

    private static IEnumerable<string> GetDependentNamespaces(IDependentMigrationOperation migrationOperation) => migrationOperation.DependsOn.Select(type => type.Namespace!);
}