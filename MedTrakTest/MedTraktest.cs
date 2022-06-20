using MedTrakModel;
using Xunit;

namespace Test
{
    public class MedicineTest
    {
        //This is how C#/XUnit recognizes that this particular method will be a unit test
        //Data annotations - They just add special metadata information that gives special properties to a particular class member

        /// <summary>
        /// Checks the validation for PokeId and sets with valid data (validData > 0)
        /// </summary>
        [Fact]
        public void MedId_Should_Set_ValidData()
        {
            //Arrange
            Medicine medObj = new Medicine();
            int medId = 28;

            //Act
            medObj.medID = medId;

            //Assert
            Assert.NotNull(medObj.medID); 
            Assert.Equal(medId, medObj.medID); 
        }


        /// <summary>
        /// Checks the validation for PokeId and checks if it fails (invalidData < 0)
        /// </summary>
        /// <param name="p_medId">The inline data being given</param>
        [Theory]
        [InlineData(-19)]
        [InlineData(-1290)]
        [InlineData(0)]
        [InlineData(-12983)]
        public void MedId_Should_Fail_Set_InvalidData(int p_medId)
        {
            //Arrange
            Medicine medObj = new Medicine();

            //Act & Assert
            Assert.Throws<System.ComponentModel.DataAnnotations.ValidationException>(() => 
                {
                    medObj.medID = p_medId;
                }
            );
        }
    }
}