using System.Collections.Generic;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Abstraction to make multiple constraints look as a single one.
    /// </summary>
    public class MacroConstraint : IApiConstraint
    {
        public IEnumerable<IApiConstraint> Constraints { get; set; }

        #region IApiConstraint Members

        public void ApplyConstraint(IWebClient client)
        {
            foreach (var constraint in Constraints)
            {
                constraint.ApplyConstraint(client);
            }
        }

        #endregion
    }
}
