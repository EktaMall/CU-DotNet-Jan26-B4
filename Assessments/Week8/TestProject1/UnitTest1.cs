using Week8_Assessment;

namespace TestProject1
{
    public class Tests
    {
        private EmployeeBonus emp;

        [SetUp]
        public void Setup()
        {
            emp = new EmployeeBonus();
        }

        [Test]
        public void NormalHighPerformer()
        {
            emp.BaseSalary = 500000;
            emp.PerformanceRating = 5;
            emp.YearsOfExperience = 6;
            emp.DepartmentMultiplier = 1.1m;
            emp.AttendancePercentage = 95;

            Assert.AreEqual(123200.00m, emp.NetAnnualBonus);
        }

        [Test]
        public void AttendancePenalty()
        {
            emp.BaseSalary = 400000;
            emp.PerformanceRating = 4;
            emp.YearsOfExperience = 8;
            emp.DepartmentMultiplier = 1.0m;
            emp.AttendancePercentage = 80;

            Assert.AreEqual(60480.00m, emp.NetAnnualBonus);
        }

        [Test]
        public void CapTriggered()
        {
            emp.BaseSalary = 1000000;
            emp.PerformanceRating = 5;
            emp.YearsOfExperience = 15;
            emp.DepartmentMultiplier = 1.5m;
            emp.AttendancePercentage = 95;

            Assert.AreEqual(280000.00m, emp.NetAnnualBonus);
        }

        [Test]
        public void ZeroSalary()
        {
            emp.BaseSalary = 0;
            emp.PerformanceRating = 5;

            Assert.AreEqual(0.00m, emp.NetAnnualBonus);
        }

        [Test]

        public void LowPerformer()
        {
            emp.BaseSalary = 300000;
            emp.PerformanceRating = 2;
            emp.YearsOfExperience = 3;
            emp.DepartmentMultiplier = 1.0m;
            emp.AttendancePercentage = 90;

            Assert.AreEqual(13500.00m, emp.NetAnnualBonus);
        }


        [Test]
        public void TaxBoundary()
        {
            emp.BaseSalary = 600000;
            emp.PerformanceRating = 3;
            emp.YearsOfExperience = 0;
            emp.DepartmentMultiplier = 1.0m;
            emp.AttendancePercentage = 100;

            Assert.AreEqual(64800.00m, emp.NetAnnualBonus);
        }

        [Test]
        public void HighTaxSlab()
        {
            emp.BaseSalary = 900000;
            emp.PerformanceRating = 5;
            emp.YearsOfExperience = 11;
            emp.DepartmentMultiplier = 1.2m;
            emp.AttendancePercentage = 100;

            Assert.AreEqual(226800.00m, emp.NetAnnualBonus);
        }

        [Test]
        public void RoundingPrecision()
        {
            emp.BaseSalary = 555555;
            emp.PerformanceRating = 4;
            emp.YearsOfExperience = 6;
            emp.DepartmentMultiplier = 1.13m;
            emp.AttendancePercentage = 92;

            Assert.AreEqual(118649.88m, emp.NetAnnualBonus);
        }

        [Test]
        public void InvalidRating()
        {
            emp.BaseSalary = 500000;
            emp.PerformanceRating = 6;

            Assert.Throws<InvalidOperationException>(() =>
            {
                var result = emp.NetAnnualBonus;
            });
        }
    }
}