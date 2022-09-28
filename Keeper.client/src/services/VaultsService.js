import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"



class VaultsService{

  async getVaultsByProfile(id){
    const res = await api.get(`api/profiles/${id}/vaults`)
    logger.log(res.data, 'PROFILE VAULTS')
    AppState.profileVaults = res.data
  }
  async getKeepsByVaultId(vaultId){
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    logger.log(res.data, 'VaultKeeps')
    AppState.vaultKeeps = res.data
  }

}

export const vaultsService = new VaultsService()