using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony;
internal class TonyEnvironment : ITonyEnvironment {
    private readonly ILogger<TonyEnvironment> logger;

    public TonyEnvironment( ILogger<TonyEnvironment> logger ) {
        this.logger = logger;
    }

    public async Task Run() {
        this.logger.LogWarning( "Hello world!" );
    }
}
