using Library.Infrastructure.Data;
using Library.UnitTests.Infrastructure;

namespace Library.UnitTests.Infrastructure.Data;

public abstract class RepositoryTestBase : IDisposable
{
    protected readonly JsonData JsonData;
    protected readonly List<string> TempFiles = new List<string>();

    protected RepositoryTestBase()
    {
        JsonData = TestDataFactory.CreateMockJsonDataWithFiles();
        
        // Keep track of temp files for cleanup
        var config = (JsonData as dynamic);
        // We'll manually track the temp files created by TestDataFactory
    }

    public void Dispose()
    {
        // Clean up temp files
        foreach (var tempFile in TempFiles)
        {
            if (File.Exists(tempFile))
            {
                try
                {
                    File.Delete(tempFile);
                }
                catch
                {
                    // Ignore cleanup errors
                }
            }
        }
    }
}
