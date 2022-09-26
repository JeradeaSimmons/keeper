import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import Pop from "../utils/Pop"
import { api } from "./AxiosService"

class ProfilesService {

async getProfile(id){
  try {
    const res = await api.get(`api/profiles/${id}`)
    AppState.profile = res.data
  } catch (error) {
    logger.error(error)
    Pop.toast(error.message, 'error')
  }
}



}
export const profilesService = new ProfilesService()