using System;
using NUnit.Framework;
using Expedia;
using Rhino.Mocks;

namespace ExpediaTest
{
	[TestFixture()]
	public class CarTest
	{	
		private Car targetCar;
		private MockRepository mocks;
		
		[SetUp()]
		public void SetUp()
		{
			targetCar = new Car(5);
			mocks = new MockRepository();
		}
		
		[Test()]
		public void TestThatCarInitializes()
		{
			Assert.IsNotNull(targetCar);
		}	
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForFiveDays()
		{
			Assert.AreEqual(50, targetCar.getBasePrice()	);
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForTenDays()
		{
            var target = new Car(10);
			Assert.AreEqual(80, target.getBasePrice());	
		}
		
		[Test()]
		public void TestThatCarHasCorrectBasePriceForSevenDays()
		{
			var target = new Car(7);
			Assert.AreEqual(10*7*.8, target.getBasePrice());
		}
		
		[Test()]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void TestThatCarThrowsOnBadLength()
		{
			new Car(-5);
		}

        [Test()]
        public void TestThatCarDoesGetCarLocationFromTheDatabase()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();

            String carLocation = "Sriram's House";
            String anotherCarLocation = "Curt's House";

            using (mocks.Record())
            {
                mockDatabase.getCarLocation(1);
                LastCall.Return(carLocation);
                mockDatabase.getCarLocation(2);
                LastCall.Return(anotherCarLocation);
            }

            var target = new Car(10);
            target.Database = mockDatabase;

            String result;

            result = target.getCarLocation(2);
            Assert.AreEqual(result, anotherCarLocation);

            result = target.getCarLocation(1);
            Assert.AreEqual(result, carLocation);
        }

        [Test()]
        public void TestThatCarDoesGetMileageFromDatabase()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            Int32 Miles = 100;

            mockDatabase.Miles = Miles;
            var target = new Car(10);
            target.Database = mockDatabase;
            int miles = target.Mileage;
            Assert.AreEqual(miles, Miles);
        }

        [Test()]
        public void TestThatCarDoesGetMileageFromDatabaseWithObjectMother()
        {
            IDatabase mockDatabase = mocks.Stub<IDatabase>();
            Int32 Miles = 100;

            mockDatabase.Miles = Miles;
            var target = ObjectMother.BMW();
            target.Database = mockDatabase;
            int miles = target.Mileage;
            Assert.AreEqual(miles, Miles);
        }


	}
}
