import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"



class KeepsService {

  async getKeeps(){
    const res = await api.get('api/keeps')
    // logger.log(res.data, 'KEEPS')
    AppState.keeps = res.data

  }

  async getKeepsByProfile(id){
    const res = await api.get(`api/profiles/${id}/keeps`)
    logger.log(res.data, 'PROFILE KEEPS')
    AppState.keeps = res.data
  }

  async getOne(id){
    const res = await api.get(`api/keeps/${id}`)
    AppState.activeKeep = res.data
    console.log('ACTIVE KEEP', res.data);
  }

  async create(keep){
    const res = await api.post('/api/keeps', keep)
    logger.log('KEEP CREATED', res.data)
    AppState.keeps.push(res.data)
  }

}
export const keepsService = new KeepsService()