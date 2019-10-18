// ASP.NET Boilerplate - Web Application Framework https://aspnetboilerplate.com
// Copyright (c) 2013-2017 Volosoft (https://volosoft.com)
// This code is licensed under MIT license (see LICENSE.txt for details)
using AspCoreMicroservice.Core.Configuration;
using AspCoreMicroservice.Core.Domain.UnitOfWork;
using AspCoreMicroservice.Core.Mapping;
using AspCoreMicroservice.Core.Security.Claims;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace AspCoreMicroservice.Core.Domain.Services
{
    public abstract class ServiceBase : IService
    {
        private IUnitOfWorkManager _unitOfWorkManager;
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager == null)
                    throw new ArgumentException("UnitOfWorkManager must be assigned before used.");

                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }

        /// <summary>
        /// Gets current unit of work.
        /// </summary>
        public ILogger Logger { protected get; set; }
        public IObjectMapper Mapper { get; set; }
        public IConfigurationManager ConfigManager { get; protected set; }

        private IClaimsPrincipalAccessor ClaimsPrincipalAccessor { get; }

        protected ClaimsPrincipal User
        {
            get { return ClaimsPrincipalAccessor?.GetCurrentPrincipal(); }
        }

        public ServiceBase(ILogger logger)
            : this(logger, null, null, null)
        {
        }

        public ServiceBase(ILogger logger, IClaimsPrincipalAccessor accessor)
            : this(logger, null, accessor, null)
        {
        }

        public ServiceBase(ILogger logger, IObjectMapper objectMapper)
            : this(logger, objectMapper, null, null)
        {
        }

        public ServiceBase(ILogger logger, IObjectMapper objectMapper, IClaimsPrincipalAccessor accessor)
          : this(logger, objectMapper, accessor, null)
        {
        }

        public ServiceBase(ILogger logger, IConfigurationManager configManager, IClaimsPrincipalAccessor accessor)
            : this(logger, null, accessor, configManager)
        {           
        }

        public ServiceBase(ILogger logger, IObjectMapper mapper, IClaimsPrincipalAccessor accessor, IConfigurationManager configManager)
        {
            Logger = logger;
            Mapper = mapper;
            ClaimsPrincipalAccessor = accessor;
            ConfigManager = configManager;
        }
    }
}
