using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BenefitCsBdd;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;

namespace BenefitCsBdd.Tests
{
    [TestClass]
    public class MultiTierBenefitTddInMemoryDb
    {

        [TestMethod]
        public void TestGetDeductibleWithInvalidProductId()
        {
            // ----------------------
            // TODO: Test the failed path
            // ----------------------
            int result = 1;

            result.Should().NotBe(2);
        }

        [TestMethod]
        public void TestGetDeductibleDdQueryOperation()
        {
            var deduct1 = new Deductible() { ProductId = "ABC00001", Level = 1, Amount = 2000 };
            var deduct2 = new Deductible() { ProductId = "ABC00001", Level = 2, Amount = 3000 };
            var conn = new SqliteConnection("DataSource=:memory:");
            var options = new DbContextOptionsBuilder<BenefitDbContext>().UseSqlite(conn).Options;

            try
            {
                conn.Open();

                // Arrange
                using (var context = new BenefitDbContext(options))
                {
                    // Create the schema and exit to force it to create immediately
                    context.Database.EnsureCreated();
                }

                // Action
                using (var context = new BenefitDbContext(options))
                {
                    // Add records to Deductible table for assertion
                    context.Deductibles.Add(deduct1);
                    context.Deductibles.Add(deduct2);
                    var writeCount = context.SaveChanges();
                }

                // Action & Asserion
                using (var context = new BenefitDbContext(options))
                {
                    var benefitDb = new BenefitRepository(context);
                    var deduct = benefitDb.GetDeductible("ABC00001");
                    deduct.FirstOrDefault<Deductible>().ProductId.Should().Be("ABC00001");
                }
            }
            catch (Exception e)
            {
                e.Message.Should().BeEmpty();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
                conn.Dispose();
            }
        }

        [TestMethod]
        public void TestDatabaseWriteOperation()
        {
            var deduct1 = new Deductible() { ProductId = "ABC00002", Level = 1, Amount = 1000 };
            var deduct2 = new Deductible() { ProductId = "ABC00002", Level = 2, Amount = 2000 };
            var conn = new SqliteConnection("DataSource=:memory:");
            conn.Open();
            var options = new DbContextOptionsBuilder<BenefitDbContext>().UseSqlite(conn).Options;

            try
            {

                // Arrange
                using (var context = new BenefitDbContext(options))
                {
                    // Create the schema and exit to force it to create immediately
                    var isCreated = context.Database.EnsureCreated();
                    isCreated.Should().BeTrue();
                }

                // Action
                using (var context = new BenefitDbContext(options))
                {
                    // Add records to Deductible table for assertion
                    context.Deductibles.Add(deduct1);
                    context.Deductibles.Add(deduct2);
                    var writeCount = context.SaveChanges();
                    writeCount.Should().Be(2);
                }
            }
            catch (Exception e)
            {
                e.Message.Should().BeEmpty();
                //e.Message.Should().NotBeEmpty();
            }
            finally
            {
                if (conn.State != ConnectionState.Closed)
                    conn.Close();
                conn.Dispose();
            }
        }
    }
}
