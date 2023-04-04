using FeatureFlag.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;

namespace FeatureFlag.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly Settings _settings;
        private readonly IFeatureManager _featureManager;

        public ValuesController(IOptionsSnapshot<Settings> options, IFeatureManager featureManager)
        {
            _settings = options.Value;
            _featureManager = featureManager;
        }

        // GET: api/<ValuesController>
        [FeatureGate(MyFeatureFlags.FeatureA)]
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            if (await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureA))
            {
                // Run the following code
            }
            if (await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureB))
            {
                // Run the following code
            }

            return new string[] { _settings.Value1, _settings.Value2 };
        }

        // GET: api/<ValuesController>
        [FeatureGate(MyFeatureFlags.FeatureB)]
        [HttpGet("Id")]
        public async Task<IEnumerable<string>> Get(int Id)
        {
            if (await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureA))
            {
                // Run the following code
            }
            if (await _featureManager.IsEnabledAsync(MyFeatureFlags.FeatureB))
            {
                // Run the following code
            }

            return new string[] { _settings.Value1, _settings.Value2 };
        }
    }
}
