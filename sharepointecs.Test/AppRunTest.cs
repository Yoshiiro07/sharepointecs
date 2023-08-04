using sharepointecs.Test.Services;

namespace sharepointecs.Test
{
    public class AppRunTest
    {
        private readonly IMockGetPageSharepoint _getPageSharepoint;
        private readonly IMockControlDBRepository _controlDBRepository;

        public AppRunTest(IMockGetPageSharepoint getPageSharepoint, IMockControlDBRepository controlDBRepository) {
            _getPageSharepoint = getPageSharepoint;
            _controlDBRepository = controlDBRepository;
        }

        [Fact]
        public void Run_OK()
        {
            var result = false;
        }
    }
}