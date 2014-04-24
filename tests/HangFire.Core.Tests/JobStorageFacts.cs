﻿using System;
using Moq;
using Xunit;

namespace HangFire.Core.Tests
{
    public class JobStorageFacts
    {
        private readonly Mock<JobStorage> _storage;

        public JobStorageFacts()
        {
            _storage = new Mock<JobStorage>() { CallBase = true };
        }

        [Fact, StaticLock(IsGlobal = true)]
        public void SetCurrent_DoesNotThrowAnException_WhenValueIsNull()
        {
            Assert.DoesNotThrow(() => JobStorage.Current = null);
        }

        [Fact, StaticLock(IsGlobal = true)]
        public void GetCurrent_ThrowsAnException_OnUninitializedValue()
        {
            JobStorage.Current = null;

            Assert.Throws<InvalidOperationException>(() => JobStorage.Current);
        }

        [Fact, StaticLock(IsGlobal = true)]
        public void GetCurrent_ReturnsCurrentValue_WhenInitialized()
        {
            var storage = new Mock<JobStorage>();
            JobStorage.Current = storage.Object;

            Assert.Same(storage.Object, JobStorage.Current);
        }

        [Fact]
        public void GetComponents_ReturnsEmptyCollectionByDefault()
        {
            Assert.Empty(_storage.Object.GetComponents());
        }

        [Fact]
        public void GetStateHandlers_ReturnsEmptyCollectionByDefault()
        {
            Assert.Empty(_storage.Object.GetStateHandlers());
        }
    }
}
