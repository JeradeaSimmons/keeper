namespace Keeper.Models
{
  public class VaultKeep : RepoItem<int>
    {
        public string CreatorId { get; set; }
        public int vaultId { get; set; }
        public int keepId { get; set; }
         public Profile Creator { get; set; }
        
    }
}