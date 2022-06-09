using OrganiserApp.ViewModels;

namespace OrganiserAppTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            var viewModel = new BaseViewModel();

            var number = viewModel.Test_Double_Number_Function(5);

            Assert.Equal(10, number);
        }
    }
}