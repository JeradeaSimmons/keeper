import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"



class VaultsService{

  async getVaultsByProfile(id){
    const res = await api.get(`api/profiles/${id}/vaults`)
    logger.log(res.data, 'PROFILE VAULTS')
    AppState.vault = res.data
  }
  async getMyVaults(){
    const res = await api.get(`/account/vaults`)
    logger.log(res.data, 'Account VAULTS')
    AppState.myVaults = res.data
  }
  async getKeepsByVaultId(vaultId){
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log( 'VaultKeeps',res.data)
    AppState.vaultKeeps = res.data
  }
  

  async create(vault){
    const res = await api.post('/api/vaults', vault)
    logger.log('VAULT CREATED', res.data)
    AppState.vault.push(res.data)
  }

}

export const vaultsService = new VaultsService()