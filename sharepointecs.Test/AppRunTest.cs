using sharepointecs.Services;

namespace sharepointecs.Test
{
    public class AppRunTest
    {
        private readonly IGetPageSharepoint _getPageSharepoint;
        private readonly IControlDBRepository _controlDBRepository;

        public AppRunTest(IGetPageSharepoint getPageSharepoint, IControlDBRepository controlDBRepository) {
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