using sharepointecs.Services;

namespace sharepointecs.Test
{
    public class AppRunTest
    {
        private readonly ISharepointServices _getPageSharepoint;
        private readonly IControlDBRepository _controlDBRepository;

        public AppRunTest(ISharepointServices getPageSharepoint, IControlDBRepository controlDBRepository) {
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