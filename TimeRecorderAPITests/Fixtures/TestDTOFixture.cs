﻿using FluentValidation;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPITests.Fixtures {
    public class TestDTOFixture {
        private readonly TestDTO _dto = new() {
            ID = Guid.NewGuid(),
            SomeProperty = "Some Property"
        };

        public TestDTO Get() {
            return _dto;
        }

        public string ID => _dto.ID.ToString()!;
    }

    public class TestDTO : IDTO {
        public Guid? ID { get; set; }
        public string SomeProperty { get; set; }
    }

    public class TestDTOValidator : AbstractValidator<TestDTO> {
        public TestDTOValidator() { }
    }
}