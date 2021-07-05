using System;
using Thesis.Domain.Commons;
using Thesis.Domain.Exceptions;

namespace Thesis.Domain.Entities
{
    public class UserAgent : BaseEntity
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }

        public string Raw
        {
            get => raw; set
            {
                if (value?.Length > RAW_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(UserAgent)}.{nameof(Raw)} cannot be bigger than {RAW_MAX_LENGTH}.");
                }
                raw = value;
            }
        }

        public string BrowserFamily
        {
            get => browserFamily; set
            {
                if (value?.Length > RAW_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(UserAgent)}.{nameof(BrowserFamily)} cannot be bigger than {BROWSER_FAMILY_MAX_LENGTH}.");
                }
                browserFamily = value;
            }
        }
        public string BrowserMajorVersion
        {
            get => browserMajorVersion; set
            {
                if (value?.Length > RAW_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(UserAgent)}.{nameof(BrowserMajorVersion)} cannot be bigger than {BROWSER_MAJOR_VERSION_MAX_LENGTH}.");
                }
                browserMajorVersion = value;
            }
        }
        public string BrowserMinorVersion
        {
            get => browserMinorVersion; set
            {
                if (value?.Length > RAW_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(UserAgent)}.{nameof(BrowserMinorVersion)} cannot be bigger than {BROWSER_MINOR_VERSION_MAX_LENGTH}.");
                }
                browserMinorVersion = value;
            }
        }

        public string OSFamily
        {
            get => oSFamily; set
            {
                if (value?.Length > RAW_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(UserAgent)}.{nameof(OSFamily)} cannot be bigger than {OS_FAMILY_MAX_LENGTH}.");
                }
                oSFamily = value;
            }
        }
        public string OSMajorVersion
        {
            get => oSMajorVersion; set
            {
                if (value?.Length > RAW_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(UserAgent)}.{nameof(OSMajorVersion)} cannot be bigger than {OS_MAJOR_VERSION_MAX_LENGTH}.");
                }
                oSMajorVersion = value;
            }
        }
        public string OSMinorVersion
        {
            get => oSMinorVersion; set
            {
                if (value?.Length > RAW_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(UserAgent)}.{nameof(OSMinorVersion)} cannot be bigger than {OS_MINOR_VERSION_MAX_LENGTH}.");
                }
                oSMinorVersion = value;
            }
        }

        public string DeviceFamily
        {
            get => deviceFamily; set
            {
                if (value?.Length > RAW_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(UserAgent)}.{nameof(DeviceFamily)} cannot be bigger than {DEVICE_FAMILY_MAX_LENGTH}.");
                }
                deviceFamily = value;
            }
        }
        public string DeviceBrand
        {
            get => deviceBrand; set
            {
                if (value?.Length > RAW_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(UserAgent)}.{nameof(DeviceBrand)} cannot be bigger than {DEVICE_BRAND_MAX_LENGTH}.");
                }
                deviceBrand = value;
            }
        }

        public string DeviceModel
        {
            get => deviceModel; set
            {
                if (value?.Length > RAW_MAX_LENGTH)
                {
                    throw new DomainLayerException($"Property {nameof(UserAgent)}.{nameof(DeviceModel)} cannot be bigger than {DEVICE_MODEL_MAX_LENGTH}.");
                }
                deviceModel = value;
            }
        }

        public bool DeviceIsSpider { get; set; }


        public static readonly int RAW_MAX_LENGTH = 450;
        public static readonly int BROWSER_FAMILY_MAX_LENGTH = 50;
        public static readonly int BROWSER_MAJOR_VERSION_MAX_LENGTH = 15;
        public static readonly int BROWSER_MINOR_VERSION_MAX_LENGTH = 15;
        public static readonly int OS_FAMILY_MAX_LENGTH = 50;
        public static readonly int OS_MAJOR_VERSION_MAX_LENGTH = 15;
        public static readonly int OS_MINOR_VERSION_MAX_LENGTH = 15;
        public static readonly int DEVICE_FAMILY_MAX_LENGTH = 50;
        public static readonly int DEVICE_BRAND_MAX_LENGTH = 50;
        public static readonly int DEVICE_MODEL_MAX_LENGTH = 50;

        private string raw;
        private string browserFamily;
        private string browserMajorVersion;
        private string browserMinorVersion;
        private string oSFamily;
        private string oSMajorVersion;
        private string oSMinorVersion;
        private string deviceFamily;
        private string deviceBrand;
        private string deviceModel;
    }
}