import { combineReducers } from 'redux'
import * as pendingRequest from './infrastructure/pendingRequest'
import * as notificationPopup from './infrastructure/notificationPopup'
import * as httpCache from './infrastructure/httpCache'

export default combineReducers({
	[pendingRequest.name]: pendingRequest.reducer,
	[notificationPopup.name]: notificationPopup.reducer,
	[httpCache.name]: httpCache.reducer,
})
