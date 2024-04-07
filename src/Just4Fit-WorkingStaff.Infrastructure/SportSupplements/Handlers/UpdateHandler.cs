namespace Just4Fit_WorkingStaff.Infrastructure.SportSupplements.Handlers;

using System;
using System.Threading.Tasks;
using Just4Fit_WorkingStaff.Core.SportSupplements.Models.Repositories;
using Just4Fit_WorkingStaff.Infrastructure.SportSupplements.Commands;


public class UpdateHandler
{
    private readonly ISportSupplementRepository supplementRepository;

    public UpdateHandler(ISportSupplementRepository supplementRepository) => this.supplementRepository = supplementRepository;

    public async Task Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request.Id);

        ArgumentNullException.ThrowIfNull(request.SportSupplement);

        await this.supplementRepository.UpdateAsync((int)request.Id, request.SportSupplement);
    }
}
