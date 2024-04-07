using FluentValidation;
using TimeRecorderAPI.Factory;
using TimeRecorderDomain;
using TimeRecorderDomain.DTO;

namespace TimeRecorderAPITests.Fixtures {
    public class TestDTOFixture {
        private readonly TestDTO _dto = new() {
            ID = Guid.NewGuid(),
            SomeProperty = "Some Property"
        };

        private readonly TestDTO _dto2 = new() {
            ID = Guid.NewGuid(),
            SomeProperty = "Some Property 2"
        };

        public TestDTO Get() {
            return _dto;
        }

        public TestDTO Get2() {
            return _dto2;
        }

        public string ID => _dto.ID.ToString()!;
    }

    public class TestFactory : IFactory<TestEntity, TestDTO> {

        public async Task<TestEntity?> Create(TestDTO? dto) {
            if (dto == null) return null;
            return await Task.FromResult(new TestEntity {
                ID = dto.ID!.ToString()!,
                SomeProperty = dto.SomeProperty
            });
        }

        public TestDTO? CreateDTO(TestEntity? entity) {
            if (entity == null) return null;
            return new TestDTO {
                ID = new Guid(entity.ID),
                SomeProperty = entity.SomeProperty
            };
        }
    }

    public class TestEntity : IDBObject {
        public string ID { get; set; }
        public string SomeProperty { get; set; }
    }

    public class TestDTO : IDTO {
        public Guid? ID { get; set; }
        public string SomeProperty { get; set; }
    }

    public class TestDTOValidator : AbstractValidator<TestDTO> {
        public TestDTOValidator() { }
    }
}